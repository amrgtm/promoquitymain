using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ApplicationPermissionDTO
{
    public class UpdatePermissionDTO
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public Int64? RoleId { get; set; }


        public bool IsGranted { get; set; }
    }
}
