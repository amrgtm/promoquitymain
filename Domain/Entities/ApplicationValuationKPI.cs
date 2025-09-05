using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationValuationKPI:CommonFields
    {
        [ForeignKey(nameof(CompanyProfile))]
        public long CompanyId { get; set; }
        public string PBRatio { get; set; }
        public string DividentYield { get; set; }
        public string Ebitda { get; set; }
        public string Description { get; set; }

        public virtual ApplicationCompanyProfile CompanyProfile { get; set; } = null!;
    }
}
