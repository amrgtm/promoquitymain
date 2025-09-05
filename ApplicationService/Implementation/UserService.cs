using Application.DTOs.ApplicationUserDTO;
using Application.Interfaces;
using ApplicationCommon;
using ApplicationService.Interface;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using ApplicationCommon.CustomException;
using Microsoft.IdentityModel.Tokens;
using ApplicationCommon.Enums;
using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.ApplicationUserRoleDTO;

namespace ApplicationService.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserRolesRepository _userRolesRepository;
        public UserService(IUserRepository userRepository,
            IUserRolesRepository userRolesRepository,
            IMapper mapper, IConfiguration configuration, IRolePermissionRepository rolePermissionRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _rolePermissionRepository = rolePermissionRepository;
            _userRolesRepository = userRolesRepository;
        }
        public async Task<UserDTO> RegisterUserAsync(CreateUserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByUserNameAsync(userDTO.UserName);
            if (existingUser != null)
                throw new DuplicateEntryException("User already exists");

            var userEntity = _mapper.Map<ApplicationUser>(userDTO);
            userEntity.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDTO.Password, 13);
            userEntity.CreatedDate = DateTime.UtcNow;
            userEntity.TenantId = userDTO.TenantId> 0 ? userDTO.TenantId : AppConstants.DefaultTenantId;

            userEntity.Gender = userDTO.Gender.ToUpper();
           

            var addedUser = await _userRepository.AddUserAsync(userEntity);
            if (addedUser != null)
            {

                var roleIds = new List<long>();

                if (!string.IsNullOrEmpty(userDTO.RoleIds))
                {
                    roleIds = userDTO.RoleIds
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => long.TryParse(r.Trim(), out var id) ? id : (long?)null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();
                }
                foreach (var role in roleIds)
                {
                    CreateUserRoleDTO cr = new CreateUserRoleDTO();
                    cr.RoleId = role;
                    cr.UserId = addedUser.Id;
                    cr.IsGranted = true;
                    await _userRolesRepository.CreateUserRoleAsync(cr);

                }
                
               
            }
            
            return _mapper.Map<UserDTO>(addedUser);
        }
        public async Task<UserDTO> GetUserProfileAsync(long? userCode = null, string? emailId = null, string? userName = null)
        {
            if (userCode == null && string.IsNullOrEmpty(emailId) && string.IsNullOrEmpty(userName))
                throw new ArgumentException("At least one identifier (userCode, emailId, or userName) must be provided.");

            ApplicationUser? appUser = null;

            if (userCode.HasValue && userCode.Value != 0)
            {
                appUser = await _userRepository.GetUserByIdAsync(userCode.Value);
            }
            else if (!string.IsNullOrEmpty(emailId))
            {
                appUser = await _userRepository.GetUserByEmailAsync(emailId);
            }
            else if (!string.IsNullOrEmpty(userName))
            {
                appUser = await _userRepository.GetUserByUserNameAsync(userName);
            }

            if (appUser == null)
                throw new NotFoundException("User not found with the provided identifier.");

            return _mapper.Map<UserDTO>(appUser);
        }


        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByUserNameAsync(loginDTO.UserName);
            if (user == null) return new LoginResponse(false, "User not found");

            bool isPasswordValid = BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.Password, user.Password);
            if (!isPasswordValid) return new LoginResponse(false, "Invalid Credentials");

            var permissions = await _rolePermissionRepository.GetPermissionByUserIdAsync(user.Id);
            if (permissions == null || !permissions.Any())
                return new LoginResponse(false, "User has no permissions assigned");

            var permissionList = permissions.Select(p => p.Name).ToList();

            string token = GenerateJWTToken(user, permissionList);
            return new LoginResponse(true, "Login successful", token, user.Id.ToString());
        }

        

        private string GenerateJWTToken(ApplicationUser user, List<string> permissions)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(AppConstants.TenantId, user.TenantId.ToString()),
            };
            claims.AddRange(permissions.Select(p => new Claim(CustomClaims.Permissions, p)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
