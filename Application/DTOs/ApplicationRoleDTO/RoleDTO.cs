namespace Application.DTOs.ApplicationRoleDTO
{
    public class RoleDTO
    {
        public long Id { get; set; }
        public required string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
