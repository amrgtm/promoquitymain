using ApplicationCommon;
using Domain.Entities;

namespace Application.DTOs.ApplicationCompanyProfileDTO
{
    public class CompanyProfileDTO:CommonFields
    {
        public required string CompanyName { get; set; }
        public string Sector { get; set; }
        public string ImageLink { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public string ImageUrl => $"/{AppConstants.ImageFolder}/{AppConstants.CompanyProfiles}/{ImageLink}";
    }
}
