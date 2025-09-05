using ApplicationCommon.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class HasPermissionAttribute:AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionNames permission):base(policy: Convert.ToString( permission)) 
        {
        }
    }
}
