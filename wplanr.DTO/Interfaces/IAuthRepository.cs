﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wplanr.Core.Models;

namespace wplanr.DTO.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponse> LoginAsync(Login login);
    }
}
