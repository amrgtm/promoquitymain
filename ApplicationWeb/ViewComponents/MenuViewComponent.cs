using ApplicationWebCore.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApplicationWeb.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IViewComponentResult Invoke()
        {
            // Extract permissions from JWT claims
            var userPermissions = ExtractPermissionsFromJwt();

            // Get filtered menu for current user
            var menuItems = _menuService.GetMenuForUser(userPermissions);

            return View(menuItems);
        }

        private List<string> ExtractPermissionsFromJwt()
        {
            var permissions = new List<string>();

            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                // Get all individual permission claims
                permissions = HttpContext.User.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value)
                    .ToList();
            }

            return permissions;
            //var permissions = new List<string>();

            //if (HttpContext.User.Identity?.IsAuthenticated ?? false)
            //{
            //    // Try to find the permission claim
            //    var permissionClaim = HttpContext.User.Claims
            //        .FirstOrDefault(c => c.Type == "permission");

            //    if (permissionClaim != null)
            //    {
            //        try
            //        {
            //            // The permission claim contains a JSON array of permissions
            //            // Example: ["User_Read", "User_Create", ...]
            //            var permissionArray = JsonSerializer.Deserialize<string[]>(permissionClaim.Value);
            //            if (permissionArray != null)
            //            {
            //                permissions.AddRange(permissionArray);
            //            }
            //        }
            //        catch (JsonException)
            //        {
            //            // Fallback: if it's not JSON, try space-separated
            //            permissions = permissionClaim.Value.Split(' ').ToList();
            //        }
            //    }
            //}

            //return permissions;
        }
    }
}