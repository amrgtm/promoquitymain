using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationUserRole : CommonFields
    {
        [ForeignKey(nameof(ApplicationRole))]
        public long RoleId { get; set; }
        public ApplicationRole? Role { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public long UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public bool IsGranted { get; set; }
    }
}
