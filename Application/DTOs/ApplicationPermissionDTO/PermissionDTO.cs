namespace Application.DTOs
{
    public class PermissionDTO
    {
        public Int64 Id { get; set; }  
        public string? Name { get; set; }
        public Int64? RoleId { get; set; }
        public bool IsGranted { get; set; }
    }
}
