﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.DTO.Models
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
