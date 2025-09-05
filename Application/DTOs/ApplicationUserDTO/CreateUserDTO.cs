using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ApplicationUserDTO
{
    public class CreateUserDTO
    {
        [Required]
        public string? UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required, Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        [Required]
        public string? MobileNo { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string? Address { get; set; } = string.Empty;
        public string? RoleIds { get; set; } = string.Empty;
        public string? PasswordResetCode { get; set; } = string.Empty;


        public string Gender { get; set; } = string.Empty;  

        public long TenantId { get; set; }
    }
}
