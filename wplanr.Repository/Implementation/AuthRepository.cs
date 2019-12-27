using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wplanr.Core.Models;
using wplanr.DTO.Interfaces;
using wplanr.DTO.Models;

namespace wplanr.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {

        public AuthRepository()
        {

        }

        public async Task<LoginResponse> LoginAsync(Login login)
        {
            var loginDto = new LoginDTO { };
            return new LoginResponse {IsLoggedIn = false,Token = loginDto.Token  };
            //return null;
        }
    }
}
