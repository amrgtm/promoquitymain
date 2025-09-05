using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApplicationUserDTO
{
    public class UserDTO:CommonFields
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public long TenantId { get; set; }
    }
}
