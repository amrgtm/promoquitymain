namespace Domain.Entities
{
    public class ApplicationRole:CommonFields
    {
        public required string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
