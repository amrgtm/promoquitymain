namespace Domain.Entities
{
    public class ApplicationBlog : CommonFields
    {
        public required string BlogTitle { get; set; }
        public required string BlogDesc { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image1Titile { get; set; }
        public string? Image2Title { get; set; }


    }
}
