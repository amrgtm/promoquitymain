using Application.Interfaces.Seeder;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.SeedData
{
    public class RoleDataSeeder : IRoleDataSeeder
    {
        private readonly AppDbContext _context;
        public RoleDataSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedRolesAsync()
        {
            if (!_context.Roles.Any())
            {
                var roles = new List<ApplicationRole>
                {
                    new ApplicationRole { RoleName = "Admin", Description = "ADMIN",TenantId=1,CreatedBy=1,CreatedDate=DateTime.UtcNow},
                    new ApplicationRole { RoleName = "User", Description = "USER",TenantId=1 ,CreatedBy=1,CreatedDate=DateTime.UtcNow}
                };
                _context.Roles.AddRange(roles);
                await _context.SaveChangesAsync();
            }
        }
    }
}
