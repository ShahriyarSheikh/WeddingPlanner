using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wplanr.Core.Interfaces;
using wplanr.Core.Models;

namespace wplanr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<bool> Checker() {
            return false;
        }

        [HttpPost,Route("Login")]
        public async Task<LoginResponse> LoginAsync(Requests.Login login) {
            return await _authService.LoginAsync(new Core.Models.Login
            {
                Password = login.Password,
                Username = login.Username
            });
        }

    }
}
