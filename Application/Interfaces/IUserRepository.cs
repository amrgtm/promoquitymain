using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(long id);
    }
}
