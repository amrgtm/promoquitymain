using ApplicationCommon.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Common
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permissios = context.User.Claims.Where(c => c.Type == CustomClaims.Permissions).Select(c => c.Value);
            if (permissios.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }return Task.CompletedTask;
        }
    }
}
