using Application.Interfaces.Seeder;
using ApplicationCommon.Enums;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.SeedData
{
    public class PermissionDataSeeder : IPermissionDataSeeder
    {
        private readonly AppDbContext _context;

        public PermissionDataSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedPermissionsAsync()
        {
            var existingPermissionNames = _context.Permissions.Select(p => p.Name).ToList();
            var allEnumPermissions = Enum.GetValues(typeof(PermissionNames)).Cast<PermissionNames>()
                .Select(p => new ApplicationPermission
                {
                    Name = p.ToString(),
                    TenantId = 1,
                    CreatedBy = 1,
                    CreatedDate = DateTime.UtcNow,
                }).ToList();

            List<ApplicationPermission> permissionsToAdd;

            if (!existingPermissionNames.Any())
            {
                permissionsToAdd = allEnumPermissions;
            }
            else
            {
                permissionsToAdd = allEnumPermissions.Where(p => !existingPermissionNames.Contains(p.Name)).ToList();
            }
            if (permissionsToAdd.Any())
            {
                _context.Permissions.AddRange(permissionsToAdd);
                await _context.SaveChangesAsync();
            }
        }


        public async Task SeedRolePermissionsAsync()
        {
            var roleId = 1;
            var createdBy = 1;
            var allPermissions = _context.Permissions.ToList();
            var existingPermissionIds = _context.RolePermissions.Where(rp => rp.RoleId == roleId).Select(rp => rp.PermissionId).ToList();

            var missingPermissions = allPermissions.Where(p => !existingPermissionIds.Contains(p.Id))
                .Select(p => new ApplicationRolePermission
                {
                    RoleId = roleId,
                    PermissionId = p.Id,
                    TenantId = 1,
                    CreatedBy = createdBy,
                    CreatedDate = DateTime.UtcNow
                }).ToList();
            if (missingPermissions.Any())
            {
                _context.RolePermissions.AddRange(missingPermissions);
                await _context.SaveChangesAsync();
            }
        }
    }
}
