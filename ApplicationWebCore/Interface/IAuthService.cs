using ApplicationCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationWebCore.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginViewModel model);
    }
}
