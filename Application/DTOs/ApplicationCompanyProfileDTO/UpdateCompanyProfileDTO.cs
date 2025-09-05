using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationCompanyProfileDTO
{
    public class UpdateCompanyProfileDTO
    {
        public long Id { get; set; }
        public required string CompanyName { get; set; }
        public string Sector { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public required IFormFile File { get; set; }
    }
}
