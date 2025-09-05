namespace Application.DTOs.ApplicationRoleDTO
{
    public class CreateRoleDTO
    {
        public required string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
