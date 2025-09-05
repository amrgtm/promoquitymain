using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class ApplicationFaq: CommonFields
    {
        public required string Question { get; set; }
        public required string Answer { get; set; }
        

    }
}
