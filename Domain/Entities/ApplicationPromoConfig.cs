using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class ApplicationPromoConfig: CommonFields
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        

    }
}
