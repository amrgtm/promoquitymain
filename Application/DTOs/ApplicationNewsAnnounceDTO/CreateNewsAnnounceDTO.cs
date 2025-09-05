using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationNewsAnnounceDTO
{
    public class CreateNewsAnnounceDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }


        public required IFormFile File { get; set; }
        public IFormFile? File2 { get; set; }


        public string? Image1 { get; set; }
        public string? Image2 { get; set; }

        public string? Image1Title { get; set; }
        public string? Image2Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Ordering { get; set; }
        public string Source { get; set; }
        public string ReadDuration { get; set; }
        public long Visit { get; set; }
        public int Priority { get; set; }
        public Int32 CreatedBy { get; set; }
        public Int32 TenantId { get; set; }
    }
}
