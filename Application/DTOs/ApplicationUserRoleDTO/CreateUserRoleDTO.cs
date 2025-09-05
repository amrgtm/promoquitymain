using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ApplicationUserRoleDTO
{
    public class CreateUserRoleDTO
    {
        [Required]
        public Int64 RoleId { get; set; }
        [Required]
        public Int64 UserId { get; set; }
        [Required]
        public bool IsGranted { get; set; }
    }
}
