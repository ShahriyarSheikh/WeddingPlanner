using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wplanr.Core.Interfaces;
using wplanr.Core.Models;
using wplanr.DTO.Interfaces;

namespace wplanr.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponse> LoginAsync(Login login)
        {
            return await _authRepository.LoginAsync(login);
        }
    }
}
