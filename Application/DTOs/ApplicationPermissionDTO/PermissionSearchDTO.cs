namespace Application.DTOs.ApplicationPermissionDTO
{
    public class PermissionSearchDTO
    {
        public Int64 Id { get; set; }
        public string? Name { get; set; }
        public Int64? RoleId { get; set; }
        public bool IsGranted { get; set; }
    }
}
