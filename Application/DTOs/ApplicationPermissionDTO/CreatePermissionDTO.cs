using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ApplicationPermissionDTO
{
    public class CreatePermissionDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public Int64? RoleId { get; set; }     
       
        public bool IsGranted { get; set; }
    }
}
