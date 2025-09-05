using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using Application.Interfaces;
using Application.Helpers;
using System.Linq.Dynamic.Core;
using Application.Interfaces.Default;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{

    public class GenericRepository<TEntity, TCreateDTO, TUpdateDTO, TReadDTO, TSearchDTO> : IGenericRepository<TEntity, TCreateDTO, TUpdateDTO, TReadDTO, TSearchDTO>
     where TEntity : class
     where TCreateDTO : class
     where TUpdateDTO : class
     where TReadDTO : class
        where TSearchDTO : class
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ICurrentUserService _currentUserService;
        public GenericRepository(AppDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<TReadDTO>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TReadDTO>>(entities);
        }
        public async Task<TReadDTO> GetByIdAsync(long id,params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            // Filter by Id
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id);

            if (entity == null)
                return default;

            return _mapper.Map<TReadDTO>(entity);
        }


        public async Task<TReadDTO> GetByIdAsync(Int64 id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TReadDTO>(entity);
        }
        //public async Task<TReadDTO?> GetByIdAsync(long id)
        //{
        //    var entity = await _dbSet.FindAsync(id);
        //    if (entity == null) return default;

        //    var dto = _mapper.Map<TReadDTO>(entity);

        //    // Optional: normalize nulls (if dto has non-nullable fields)
        //    foreach (var prop in typeof(TReadDTO).GetProperties())
        //    {
        //        if (prop.PropertyType == typeof(string) &&
        //            prop.GetValue(dto) == null)
        //        {
        //            prop.SetValue(dto, string.Empty);
        //        }
        //    }

        //    return dto;
        //}

        /// <summary>
        /// Pass the column Name with operator and value
        /// </summary>
        /// <param name="pagedRequest"></param>
        /// <returns></returns>
        public async Task<TReadDTO> GetByColumns(PagedRequestDto pagedRequest)
        {
            // Start with the base query
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // 2. Apply Filters
            if (pagedRequest.Filters != null && pagedRequest.Filters.Any())
            {
                foreach (var filter in pagedRequest.Filters)
                {
                    // Build the filter dynamically
                    var filterExpression = "";
                    if (filter.Operator.Contains("Contain") || filter.Operator.Contains("like"))
                    {
                        filterExpression = $"{filter.FieldName}.Contains(@0)"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("="))
                    {
                        filterExpression = $"{filter.FieldName}=@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains(">"))
                    {
                        filterExpression = $"{filter.FieldName}>@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("<"))
                    {
                        filterExpression = $"{filter.FieldName}<@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("<>"))
                    {
                        filterExpression = $"{filter.FieldName}<>@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    query = query.Where(filterExpression, filter.Value);
                }
            }

            // 3. Calculate the total number of records (after applying filters)
            var totalRecords = await query.CountAsync();

            // 4. Apply pagination
            var entities = query.ToListAsync().Result.FirstOrDefault();

            // 5. Map entities to DTOs
            var dtos = _mapper.Map<TReadDTO>(entities);

            // 6. Return paginated result
            return dtos;
        }
        public async Task<long> AddAsync(TCreateDTO createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            if (entity is CommonFields common)
            {
                common.CreatedDate = DateTime.UtcNow;
                common.CreatedBy = _currentUserService.UserId;
                common.TenantId = common.TenantId == 0 ? _currentUserService.TenantId : common.TenantId;
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            var entityId = (entity as dynamic).Id; // Assuming all entities have an Id property
            return entityId;
        }

        public async Task UpdateAsync(TUpdateDTO updateDto)
        {
            // Map DTO to an entity instance
            var entity = _mapper.Map<TEntity>(updateDto);

            // Check if the entity is already being tracked
            var trackedEntity = await _dbSet.FindAsync(GetPrimaryKeyValue(entity));
            if (trackedEntity != null)
            {
                // Preserve CreatedDate and GuId from tracked entity
                if (trackedEntity is CommonFields commonTracked && entity is CommonFields commonEntity)
                {
                    commonEntity.CreatedDate = commonTracked.CreatedDate;
                }
                _context.Entry(trackedEntity).CurrentValues.SetValues(entity);
                // Set ModifiedDate on tracked entity explicitly
                if (trackedEntity is CommonFields commonTrackedEntity)
                {
                    commonTrackedEntity.ModifiedDate = DateTime.UtcNow;
                }
            }
            else
            {
                if (entity is CommonFields common)
                {
                    common.ModifiedDate = DateTime.UtcNow;
                }
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            // Save changes
            await _context.SaveChangesAsync();
        }

        // Helper method to retrieve the primary key value
        private object GetPrimaryKeyValue(TEntity entity)
        {
            var keyName = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey()
                .Properties
                .Select(x => x.Name)
                .Single();

            return entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }


        public async Task DeleteAsync(Int64 id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<TReadDTO>> GetPagedAsync(int pageIndex, int pageSize)
        {
            try
            {

                await _context.Database.OpenConnectionAsync();
                _context.Database.CloseConnection();
                // Database connection succeeded
                Console.WriteLine("connected");

            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Database connection failed: {ex.Message}");

            }
            var totalRecords = await _dbSet.CountAsync();
            var entities = await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var dtos = _mapper.Map<List<TReadDTO>>(entities);
            return new PaginatedList<TReadDTO>(dtos, totalRecords, pageIndex, pageSize);
        }
        public async Task<PaginatedList<TReadDTO>> GetPagedAsync(PagedRequestDto pagedRequest)
        {
            // Start with the base query
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            // 2. Apply Filters
            if (pagedRequest.Filters != null && pagedRequest.Filters.Any())
            {
                foreach (var filter in pagedRequest.Filters)
                {
                    // Build the filter dynamically
                    var filterExpression = "";
                    if (filter.Operator.Contains("Contain") || filter.Operator.Contains("like"))
                    {
                        filterExpression = $"{filter.FieldName}.Contains(@0)"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("="))
                    {
                        filterExpression = $"{filter.FieldName}=@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains(">"))
                    {
                        filterExpression = $"{filter.FieldName}>@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("<"))
                    {
                        filterExpression = $"{filter.FieldName}<@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    if (filter.Operator.Contains("<>"))
                    {
                        filterExpression = $"{filter.FieldName}<>@0"; //$"{filter.FieldName}.{filter.Operator} @0";
                        //query =query.Where(filterExpression)
                    }
                    query = query.Where(filterExpression, filter.Value);
                }
            }

            // 3. Calculate the total number of records (after applying filters)
            var totalRecords = await query.CountAsync();

            // 4. Apply pagination
            var entities = await query
                .Skip((pagedRequest.PageIndex - 1) * pagedRequest.PageSize)
                .Take(pagedRequest.PageSize)
                .ToListAsync();

            // 5. Map entities to DTOs
            var dtos = _mapper.Map<List<TReadDTO>>(entities);

            // 6. Return paginated result
            return new PaginatedList<TReadDTO>(dtos, totalRecords, pagedRequest.PageIndex, pagedRequest.PageSize);
        }

        #region for rawqueries
        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<IEnumerable<TReadDTO>> QuerySqlRawAsync(string sql, params object[] parameters)
        {
            var entities = await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
            return _mapper.Map<IEnumerable<TReadDTO>>(entities);
        }

        public async Task<PaginatedList<TSearchDTO>> QuerySqlGeneralRawPaginatedAsync<TSearchDTO>(
        string baseSql,
        int pageIndex,
        int pageSize,
        string orderBy = "Id",
        bool ascending = true,
        string ComputationParams = null,
        string countquery = null, bool closedbConnection = false,
        CommandType cmdType = CommandType.Text,
        params SqlParameter[] parameters)
        {
            // Step 1: Build SQL query with pagination
            string orderClause = ascending ? "ASC" : "DESC";
            string paginatedSql = "";

            if (!string.IsNullOrEmpty(orderBy))
            {

                paginatedSql = $@"{ComputationParams}{baseSql}
    ORDER BY {orderBy} {orderClause}
    OFFSET {(pageIndex - 1) * pageSize} ROWS
    FETCH NEXT {pageSize} ROWS ONLY";
            }
            else
            {
                paginatedSql = $@"{ComputationParams}{baseSql}
        OFFSET {(pageIndex - 1) * pageSize} ROWS
        FETCH NEXT {pageSize} ROWS ONLY";
            }

            string countSql = $@"{ComputationParams}
                    SELECT COUNT(1)
                    FROM ({baseSql}) AS CountQuery";
            if (!string.IsNullOrEmpty(countquery))
            {
                countSql = $@"{ComputationParams}
                    {countquery} AS CountQuery";
            }
            var results = new List<TSearchDTO>();
            int totalRecords;



            DbConnection connection = null;
            try
            {
                connection = _context.Database.GetDbConnection();

                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                // Step 2: Execute the paginated query
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = paginatedSql;
                    command.CommandType = cmdType;
                    // Add cloned parameters
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(CloneParameter(param));
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var mapper = new Mapper<TSearchDTO>();
                        while (await reader.ReadAsync())
                        {
                            results.Add(mapper.Map(reader));
                        }
                    }

                    command.CommandText = countSql;
                    totalRecords = Convert.ToInt32(await command.ExecuteScalarAsync());
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while executing the paginated query.", ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            // Step 4: Return the paginated list
            return new PaginatedList<TSearchDTO>(results, totalRecords, pageIndex, pageSize);
        }
        public async Task<List<TReadDTO>> QuerySqlGeneralRawAsync<TReadDTO>(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            var results = new List<TReadDTO>();

            var connection = _context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = cmdType;
                foreach (var param in parameters)
                {
                    command.Parameters.Add(CloneParameter(param));
                }
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var mapper = new Mapper<TReadDTO>();
                    while (await reader.ReadAsync())
                    {
                        results.Add(mapper.Map(reader));
                    }
                }
            }

            return results;
        }
        public int CountRecord(string baseSql, params SqlParameter[] parameters)
        {
            string paginatedSql = $@"SELECT COUNT(1) FROM ({baseSql}) AS CountQuery";
            int totalRecords = 0;

            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    connection.OpenAsync();
                    // Step 2: Execute the paginated query
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = paginatedSql;

                        // Add cloned parameters
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(CloneParameter(param));
                        }

                        totalRecords = Convert.ToInt32(command.ExecuteScalarAsync());
                    }
                    return totalRecords;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while executing the paginated query.", ex);
            }

            // Step 4: Return the paginated list

        }
        // Helper method to clone SqlParameter
        private SqlParameter CloneParameter(SqlParameter param)
        {
            return new SqlParameter(param.ParameterName, param.Value)
            {
                DbType = param.DbType,
                Direction = param.Direction,
                IsNullable = param.IsNullable,
                Size = param.Size,
                Precision = param.Precision,
                Scale = param.Scale,
                SourceColumn = param.SourceColumn,
                SourceVersion = param.SourceVersion
            };
        }
        #endregion
        public async Task<PaginatedList<TReadDTO>> GetWithJoinsAsync<TJoinEntity>(Func<IQueryable<TEntity>, IQueryable<TJoinEntity>> joinQuery, int pageIndex, int pageSize, List<FilterCriteriaDto>? filters = null)
        {
            // Apply the join query
            IQueryable<TJoinEntity> query = joinQuery(_dbSet);
            // 1. Apply Filters only if filters are provided
            if (filters != null && filters.Any())
            {
                query = ApplyFilters(query, filters);
            }
            // 2. Calculate total records after filtering (or without filters if none applied)
            int totalRecords = await query.CountAsync();
            // 3. Apply Pagination
            var entities = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            // 4. Map entities to DTOs
            var dtos = _mapper.Map<List<TReadDTO>>(entities);
            // 5. Return Paginated List
            return new PaginatedList<TReadDTO>(dtos, totalRecords, pageIndex, pageSize);
        }

        // Helper function to dynamically apply filters
        private IQueryable<T> ApplyFilters<T>(IQueryable<T> query, List<FilterCriteriaDto> filters)
        {
            foreach (var filter in filters)
            {
                string filterExpression = filter.Operator switch
                {
                    "Contains" => $"{filter.FieldName}.Contains(@0)",
                    "=" => $"{filter.FieldName} == @0",
                    ">" => $"{filter.FieldName} > @0",
                    "<" => $"{filter.FieldName} < @0",
                    _ => throw new ArgumentException($"Unknown operator: {filter.Operator}")
                };
                query = query.Where(filterExpression, filter.Value);
            }
            return query;
        }

        public async Task<int> ExecuteNonQueryAsync(string sqlOrSpName, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                var connString = _context.Database.GetDbConnection().ConnectionString;
                await using (var connection = new SqlConnection(connString))
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    await using (var command = new SqlCommand(sqlOrSpName, connection))
                    {
                        command.CommandType = cmdType;

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.Add(CloneParameter(param));
                            }
                        }
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing non-query: {sqlOrSpName}", ex);
            }
        }
        public async Task<int> ExecuteNonQueryWithOutputAsync(string sqlOrSpName, CommandType cmdType, params SqlParameter[] parameters)
        {
            try
            {
                var connString = _context.Database.GetDbConnection().ConnectionString;

                await using (var connection = new SqlConnection(connString))
                {
                    if (connection.State != ConnectionState.Open)
                        await connection.OpenAsync();

                    await using (var command = new SqlCommand(sqlOrSpName, connection))
                    {
                        command.CommandType = cmdType;

                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(CloneParameter(param));
                        }

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        for (int i = 0; i < command.Parameters.Count; i++)
                        {
                            if (command.Parameters[i] is SqlParameter sqlParam &&
                                (sqlParam.Direction == ParameterDirection.Output ||
                                 sqlParam.Direction == ParameterDirection.InputOutput ||
                                 sqlParam.Direction == ParameterDirection.ReturnValue))
                            {
                                parameters[i].Value = sqlParam.Value;
                            }
                        }
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing non-query: {sqlOrSpName} {ex.Message.ToLower()}", ex);
            }
        }

    }
    public class Mapper<T>
    {
        public T Map(DbDataReader reader)
        {
            var properties = typeof(T).GetProperties();
            var instance = Activator.CreateInstance<T>();

            foreach (var prop in properties)
            {
                if (reader.HasColumn(prop.Name) && !reader.IsDBNull(prop.Name))
                {
                    prop.SetValue(instance, reader[prop.Name]);
                }
            }

            return instance;
        }
    }

    public static class DbDataReaderExtensions
    {
        public static bool HasColumn(this DbDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDBNull(this DbDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName));
        }
    }

}
