using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationMarketKPI:CommonFields
    {
        [ForeignKey(nameof(CompanyProfile))]
        public long CompanyId { get; set; }
        public string Beta { get; set; }
        public string LowMarketCap { get; set; }
        public string HighMarketCap { get; set; }
        public string MedMarketCap { get; set; }
        public string RetailSales { get; set; }
        public string ActiveUsers { get; set; }
        public string Volume { get; set; }
        public string Duedegi { get; set; }
        public string Description { get; set; }
        public virtual ApplicationCompanyProfile CompanyProfile { get; set; } = null!;
    }
}
