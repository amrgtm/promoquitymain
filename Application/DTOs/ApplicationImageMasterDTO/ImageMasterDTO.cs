using ApplicationCommon;
using Domain.Entities;

namespace Application.DTOs.ApplicationImageMasterDTO
{
    public class ImageMasterDTO : CommonFields
    {
        public required string ImageName { get; set; }
        public required string Source { get; set; }
        public string? ImageFieldName { get; set; }
        public string? ImageTitle { get; set; }
        public bool IsThumbnail { get; set; }
        public required long TableId { get; set; }
        public string ImageUrl => $"/{AppConstants.ImageFolder}/{Source}/{ImageName}";
    }
}
