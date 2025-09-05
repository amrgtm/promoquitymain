using Application.Interfaces.Seeder;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.SeedData
{
    public class UserDataSeeder : IUserDataSeeder
    {
        private readonly AppDbContext _context;

        public UserDataSeeder(AppDbContext context)
        {
            this._context = context;
        }
        public async Task SeedDefaultUserAsync()
        {
            if (!_context.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    TenantId = 1,
                    UserName = "admin",
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword("admin@123", 13), //"admin@123",
                    Email = "admin@promoquity.com",
                    FirstName = "Admin",
                    LastName = "User",
                    Address = "Kathmandu, Nepal",
                    MobileNo = "9800000000",
                    CreatedBy = 1,
                    DOB = DateTime.Today.AddYears(-20),
                    CreatedDate = DateTime.UtcNow,
                    Gender = "Male"
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
