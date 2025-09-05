using Application.Interfaces.Default;
using ApplicationCommon;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UserId =>
                long.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value, out var id)
                ? id
                : 0;

        public long TenantId =>
            long.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst(AppConstants.TenantId)?.Value, out var tid)
                ? tid
                : AppConstants.DefaultTenantId;
    }
}
