namespace Application.DTOs.ApplicationDownloadDTO
{
    public class UpdateDownloadDTO
    {
        public required int Id { get; set; }
        public required string DocTitle { get; set; }
        public string DocDesc { get; set; }
        public string DocName { get; set; }
        public bool IsVisible { get; set; }
        public int Ordering { get; set; }
    }
}
