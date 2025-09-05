using ApplicationCommon;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseLogger(string categoryName, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _categoryName = categoryName;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error || logLevel == LogLevel.Critical;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            _ = Task.Run(() => LogToDatabaseAsync(logLevel, formatter(state, exception), exception));
        }

        private async Task LogToDatabaseAsync(LogLevel logLevel, string message, Exception exception)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var httpContext = _httpContextAccessor.HttpContext;

                var errorLog = new ErrorLog
                {
                    CreatedDate = DateTime.UtcNow,
                    LogLevel = logLevel.ToString(),
                    Source = _categoryName,
                    Message = message,
                    Exception = exception?.ToString(),
                    StackTrace = exception?.StackTrace,
                };

                if (httpContext != null)
                {
                    var user = httpContext.User;
                    if (user?.Identity?.IsAuthenticated == true)
                    {
                        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
                        if (int.TryParse(userIdClaim, out int parsedUserId))
                        {
                            errorLog.CreatedBy = parsedUserId;
                        }
                    }
                    errorLog.RequestPath = httpContext.Request?.Path.ToString() ?? AppConstants.Unknown;
                    errorLog.HttpMethod = httpContext.Request?.Method ?? AppConstants.Unknown;
                    errorLog.UserAgent = httpContext.Request?.Headers["User-Agent"].ToString() ?? AppConstants.Unknown;
                    errorLog.IpAddress = httpContext.Connection?.RemoteIpAddress?.ToString() ?? AppConstants.Unknown;
                }

                context.ErrorLogs.Add(errorLog);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log error to database: {ex.Message}");
            }
        }
        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
