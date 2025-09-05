using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationPromoConfigDTO
{
    public class CreatePromoConfigDTO
    {
       
        public required string Title { get; set; }
        public required string Description { get; set; }    
    }
}
