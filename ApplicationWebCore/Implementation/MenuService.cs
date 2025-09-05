
using ApplicationCommon;
using ApplicationWebCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationWebCore.Implementation
{
    public class MenuService : IMenuService
    {
        private readonly List<MenuItemDTO> _fullMenu = new()
    {
        new() {
            Title = "Dashboard",
            Icon = "fas fa-home",
            Path = "/Home/Dashboard",
            RequiredPermission = "User_Read" // Example permission
        },
        new() {
            Title = "User Management",
            Icon = "nav-icon bi bi-palette",
            Path = "/Admin/Users",
            RequiredPermission = "User_Read"
        },
        new() {
            Title = "Role Management",
            Icon = "fas fa-user-shield",
            Path = "/Admin/Roles",
            RequiredPermission = "Role_Read"
        },
        new() {
            Title = "Permission Management",
            Icon = "fas fa-key",
            Path = "/Admin/Permissions",
            RequiredPermission = "Permission_Read"
        },
        new() {
            Title = "Category Management",
            Icon = "fas fa-folder",
            Path = "/Admin/Categories",
            RequiredPermission = "Category_Read"
        },
        new() {
            Title = "Question Bank",
            Icon = "fas fa-book",
            RequiredPermission = "Question_Read",
            Children = new()
            {
                new() {
                    Title = "Questions",
                    Path = "/Questions",
                    RequiredPermission = "Question_Read"
                },
                new() {
                    Title = "Topics",
                    Path = "/Topics",
                    RequiredPermission = "Topic_Read"
                },
                new() {
                    Title = "Skills",
                    Path = "/Skills",
                    RequiredPermission = "Skill_Read"
                }
            }
        },
        new() {
            Title = "Exam Management",
            Icon = "fas fa-graduation-cap",
            RequiredPermission = "Exam_Read",
            Children = new()
            {
                new() {
                    Title = "Exams",
                    Path = "/Exams",
                    RequiredPermission = "Exam_Read"
                },
                new() {
                    Title = "Exam Results",
                    Path = "/ExamResults",
                    RequiredPermission = "UserQuestionHistory_Read"
                }
            }
        },
        new() {
            Title = "System Settings",
            Icon = "fas fa-cogs",
            Path = "/Admin/Settings",
            RequiredPermission = "User_Update" // Higher privilege required
        },
        new() {
            Title = "My Profile",
            Icon = "fas fa-user",
            Path = "/Profile"
            // No permission required - accessible to all authenticated users
        }
    };

        public List<MenuItemDTO> GetMenuForUser(List<string> userPermissions)
        {
            return BuildFilteredMenu(_fullMenu, userPermissions);
        }

        private List<MenuItemDTO> BuildFilteredMenu(List<MenuItemDTO> sourceMenu, List<string> userPermissions)
        {
            var filteredMenu = new List<MenuItemDTO>();

            foreach (var menuItem in sourceMenu)
            {
                // Check if user has permission for this specific menu item
                bool canAccessMenuItem = CanUserAccessMenuItem(menuItem, userPermissions);

                if (!canAccessMenuItem)
                    continue;

                // Create a copy of the menu item with filtered children
                var filteredItem = new MenuItemDTO
                {
                    Title = menuItem.Title,
                    Icon = menuItem.Icon,
                    Path = menuItem.Path,
                    RequiredPermission = menuItem.RequiredPermission
                };

                // Recursively filter children menu items
                if (menuItem.Children.Any())
                {
                    filteredItem.Children = BuildFilteredMenu(menuItem.Children, userPermissions);

                    // Only add parent item if it has visible children
                    if (!filteredItem.Children.Any())
                        continue;
                }

                filteredMenu.Add(filteredItem);
            }

            return filteredMenu;
        }

        private bool CanUserAccessMenuItem(MenuItemDTO menuItem, List<string> userPermissions)
        {
            // If no permission is required, always allow access
            if (string.IsNullOrEmpty(menuItem.RequiredPermission))
                return true;

            // Check if user has the required permission
            return userPermissions.Contains(menuItem.RequiredPermission);
        }
    }
}