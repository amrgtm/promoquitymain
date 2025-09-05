namespace Domain.Entities
{
    public class ApplicationUser : CommonFields
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? PasswordResetCode { get; set; }
        public DateTime? DOB { get; set; }
        public required string Gender { get; set; }
    }
}
