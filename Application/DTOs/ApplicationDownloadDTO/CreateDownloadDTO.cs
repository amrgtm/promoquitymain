namespace Application.DTOs.ApplicationDownloadDTO
{
    public class CreateDownloadDTO
    {
        public required string DocTitle { get; set; }
        public string DocDesc { get; set; }
        public string DocName { get; set; }
        public bool IsVisible { get; set; }
        public int Ordering { get; set; }
    }
}
