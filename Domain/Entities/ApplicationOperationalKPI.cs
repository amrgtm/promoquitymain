using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationOperationalKPI : CommonFields
    {
        [ForeignKey(nameof(CompanyProfile))]
        public required long CompanyId { get; set; }
        public string OperatingMargin { get; set; }
        public string InvTurnOver { get; set; }
        public string Description { get; set; }
        public virtual ApplicationCompanyProfile CompanyProfile { get; set; } = null!;
    }
}
