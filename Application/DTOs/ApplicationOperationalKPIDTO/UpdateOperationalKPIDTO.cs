namespace Application.DTOs.ApplicationOperationalKPIDTO
{
    public class UpdateOperationalKPIDTO
    {
        public required long Id { get; set; }
        public required long CompanyId { get; set; }
        public string OperatingMargin { get; set; }
        public string InvTurnOver { get; set; }
        public string Description { get; set; }
    }
}
