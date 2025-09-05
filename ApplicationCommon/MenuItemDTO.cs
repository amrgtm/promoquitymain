using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCommon
{
    public class MenuItemDTO
    {
        public string Title { get; set; }
        public string? Icon { get; set; }
        public string? Path { get; set; }
        public string? RequiredPermission { get; set; }
        public List<MenuItemDTO> Children { get; set; } = new();
    }
}
