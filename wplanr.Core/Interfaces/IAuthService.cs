using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wplanr.Core.Models;

namespace wplanr.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(Login login);
    }
}
