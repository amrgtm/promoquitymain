using ApplicationCommon;
using Domain.Entities;

namespace Application.DTOs.ApplicationFaqDTO
{
    public class FaqDTO : CommonFields
    {
        public required string Question { get; set; }
        public required string Answer { get; set; }
    }
}
