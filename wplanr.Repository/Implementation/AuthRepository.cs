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
        private readonly IMongoAdapter _mongoAdapter;

        public AuthRepository(IMongoAdapter mongoAdapter)
        {
            _mongoAdapter = mongoAdapter;
        }

        public async Task<LoginResponse> LoginAsync(Login login)
        {
            var loginDto = new LoginDTO { IsLoggedIn = false };
            var result = await _mongoAdapter.InsertOneAsync<LoginDTO>(loginDto, "User");
            return new LoginResponse {IsLoggedIn = result, Token = loginDto.Token  };
        }
    }
}
