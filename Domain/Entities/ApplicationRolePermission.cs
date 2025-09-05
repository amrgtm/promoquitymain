using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationRolePermission : CommonFields
    {
        [ForeignKey(nameof(Role))]
        public long RoleId { get; set; }
        [ForeignKey(nameof(Permission))]
        public long PermissionId { get; set; }
        public virtual ApplicationRole? Role { get; set; }
        public virtual ApplicationPermission? Permission { get; set; }
    }
}
