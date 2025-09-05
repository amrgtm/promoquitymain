using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationFinancialKPI:CommonFields
    {
        [ForeignKey(nameof(CompanyProfile))]
        public required long CompanyId { get; set; }
        public string EPS { get; set; }
        public string PERatio { get; set; }
        public string ROE { get; set; }
        public string ROA { get; set; }
        public string NetProfitMargin { get; set; }
        public string Description { get; set; }
        public virtual ApplicationCompanyProfile CompanyProfile { get; set; } = null!;
    }
}
