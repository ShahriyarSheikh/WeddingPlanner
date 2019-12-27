using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.Core.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
