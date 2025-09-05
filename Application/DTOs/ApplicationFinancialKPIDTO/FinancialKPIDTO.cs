using Domain.Entities;

namespace Application.DTOs.ApplicationFinancialKPIDTO
{
    public class FinancialKPIDTO: CommonFields
    {
        public required long CompanyId { get; set; }
        public string EPS { get; set; }
        public string PERatio { get; set; }
        public string ROE { get; set; }
        public string ROA { get; set; }
        public string NetProfitMargin { get; set; }
        public string Description { get; set; }
    }
}
