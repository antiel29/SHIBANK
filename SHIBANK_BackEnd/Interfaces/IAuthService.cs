﻿using Microsoft.AspNetCore.Authentication;
using SHIBANK.Models;
using SHIBANK.Helper;

namespace SHIBANK.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        string GenerateToken(User user);
    }
}
