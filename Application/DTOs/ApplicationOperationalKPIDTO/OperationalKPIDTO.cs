using Domain.Entities;

namespace Application.DTOs.ApplicationOperationalKPIDTO
{
    public class OperationalKPIDTO:CommonFields
    {
        public required long CompanyId { get; set; }
        public string OperatingMargin { get; set; }
        public string InvTurnOver { get; set; }
        public string Description { get; set; }
    }
}
