using Application.DTOs.ApplicationOperationalKPIDTO;
using Application.Helpers;

namespace Application.Interfaces
{
    public interface IOperationalKPIRepository
    {
        Task<OperationalKPIDTO> CreateOperationKPIAsync(CreateOperationalKPIDTO createOperationalKPIDTO);
        Task<OperationalKPIDTO> UpdateOperationKPIAsync(UpdateOperationalKPIDTO updateOperationalKPIDTO);
        Task<OperationalKPIDTO> DeleteOperationKPIAsync(long id);
        Task<OperationalKPIDTO> GetOperationKPIByIdAsync(long id);
        Task<PaginatedList<OperationalKPIDTO>> GetPagedOperationKPIListAsync(int pageIndex, int pageSize, string companyId = null);
    }
}
