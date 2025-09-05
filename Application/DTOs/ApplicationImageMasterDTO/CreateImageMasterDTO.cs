using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationImageMasterDTO
{
    public class CreateImageMasterDTO
    {
        public string? ImageName { get; set; }
        public required string Source { get; set; }
        public string? ImageFieldName { get; set; }        
        public string? ImageTitle { get; set; }
        public bool IsThumbnail { get; set; }
        public required long TableId { get; set; }
        public required IFormFile File { get; set; }
    }
}
