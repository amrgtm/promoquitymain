using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationHomeContentDTO
{
    public class UpdateHomeContentDTO
    {
        public long Id { get; set; }
        public required string Topic { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public required IFormFile File { get; set; }
    }
}
