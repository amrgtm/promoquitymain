using Application.DTOs.ApplicationUserDTO;
using ApplicationCommon;

namespace ApplicationService.Interface
{
    public interface IUserService
    {
        Task<UserDTO> RegisterUserAsync(CreateUserDTO userDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
        Task<UserDTO> GetUserProfileAsync(long? userCode, string emailId = null, string userName = null);
    }
}
