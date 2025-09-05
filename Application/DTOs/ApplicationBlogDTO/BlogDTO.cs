using Application.DTOs.ApplicationImageMasterDTO;
using Domain.Entities;

namespace Application.DTOs.ApplicationBlogDTO
{
    public class BlogDTO:CommonFields
    {
        public required string BlogTitle { get; set; }
        public required string BlogDesc { get; set; }
        public string? Image1 { get; set; } = string.Empty;
        public string? Image2 { get; set; } = string.Empty;
        public string? Image1Titile { get; set; } = string.Empty;
         public string? Image2Title { get; set; }= string.Empty;    
        public List<ImageMasterDTO> Images { get; set; } = new();
    }
}
