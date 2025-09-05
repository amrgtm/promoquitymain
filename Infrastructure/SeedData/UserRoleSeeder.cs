using Application.Interfaces.Seeder;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.SeedData
{
    public class UserRoleSeeder : IUserRoleSeeder
    {
        private readonly AppDbContext _context;

        public UserRoleSeeder(AppDbContext context)
        {
            this._context = context;
        }
        public async Task SeedDefaultUserRoleAsync()
        {
            if (!_context.UserRoles.Any())
            {
                var userRole = new ApplicationUserRole()
                {
                    RoleId = 1,
                    UserId = 1,
                    IsGranted = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.UtcNow,
                    TenantId = 1
                };
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }
}
