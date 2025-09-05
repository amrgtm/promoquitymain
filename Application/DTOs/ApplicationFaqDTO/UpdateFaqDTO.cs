using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationFaqDTO
{
    public class UpdateFaqDTO
    {
        public long Id { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
    }
}
