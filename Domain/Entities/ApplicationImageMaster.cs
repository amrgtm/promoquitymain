using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class ApplicationImageMaster:CommonFields
    {
        public required string ImageName { get; set; }
        public required string TableName { get; set; }
        public string? ImageFieldName { get; set; }
        public string? ImageTitle { get; set; }
        public bool IsThumbnail { get; set; }
        public required long TableId { get; set; }

    }
}
