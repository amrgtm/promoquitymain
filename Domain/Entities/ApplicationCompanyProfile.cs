namespace Domain.Entities
{
    public class ApplicationCompanyProfile:CommonFields
    {
        public required string CompanyName { get; set; }
        public string Sector { get; set; }
        public string? ImageLink { get; set; }
        public string? Remarks1 { get; set; }
        public string? Remarks2 { get; set; }
        public string? Remarks3 { get; set; }
    }
}
