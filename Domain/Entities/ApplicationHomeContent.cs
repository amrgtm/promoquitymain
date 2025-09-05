namespace Domain.Entities
{
    public class ApplicationHomeContent:CommonFields
    {
        public required string Topic { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public string ImageLink { get; set; }
    }
}
