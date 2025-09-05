using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ApplicationPromoConfigDTO
{
    public class UpdatePromoConfigDTO
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
