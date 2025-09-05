
using ApplicationCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationWebCore.Interface
{
    public interface IMenuService
    {
        List<MenuItemDTO> GetMenuForUser(List<string> userPermissions);
    }
}
