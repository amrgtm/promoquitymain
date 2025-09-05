using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _appDbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<ApplicationUser?> GetUserByUserNameAsync(string userName)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(long id)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
