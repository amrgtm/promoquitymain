using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationHomeContentMidDTO
{
    public class CreateHomeContentMidDTO
    {
        public required string Topic { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public required IFormFile File { get; set; }
    }
}
