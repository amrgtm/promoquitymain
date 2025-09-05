using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationBlogDTO
{
    public class CreateBlogDTO
    {
        public required string BlogTitle { get; set; }
        public required string BlogDesc { get; set; }
        public required IFormFile File { get; set; }
        public  IFormFile? File2 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image1Titile { get; set; }
        public string Image2Title { get; set; }
        public long TenantId { get; set; }  
        public Int32 CreatedBy { get; set; }   
    }
}
