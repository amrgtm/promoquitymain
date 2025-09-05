using ApplicationCommon;
using Domain.Entities;

namespace Application.DTOs.ApplicationPromoConfigDTO
{
    public class PromoConfigDTO : CommonFields
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
