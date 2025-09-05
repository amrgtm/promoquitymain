using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationFaqDTO
{
    public class CreateFaqDTO
    {
       
        public required string Question { get; set; }
        public required string Answer { get; set; }    
    }
}
