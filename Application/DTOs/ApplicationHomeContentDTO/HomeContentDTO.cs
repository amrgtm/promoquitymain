using ApplicationCommon;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationHomeContentDTO
{
    public class HomeContentDTO:CommonFields
    {
        public required string Topic { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public string ImageLink { get; set; }
        public required IFormFile File { get; set; }
        public string ImageUrl => $"/{AppConstants.ImageFolder}/{AppConstants.HomeContents}/{ImageLink}";
    }
}
