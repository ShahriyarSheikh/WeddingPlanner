using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wplanr.DTO.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> LoginAsync();
    }
}
