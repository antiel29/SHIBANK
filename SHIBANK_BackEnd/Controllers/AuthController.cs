﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Services;

namespace SHIBANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly TokenBlacklist _tokenBlacklist;


        public AuthController(IAuthService authService, TokenBlacklist tokenBlacklist)
        {
            _authService = authService;
            _tokenBlacklist = tokenBlacklist;
        }

        //Create a JWT token, if 30 min innactive then expires
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDto userloginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResult = _authService.Authenticate(userloginDto.Username, userloginDto.Password);

            if (!authResult.Success)
                return Unauthorized();

            return Ok(new { token = authResult.Token });
        }

        //Logout
        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                if (_tokenBlacklist.IsTokenBlacklisted(token))
                {
                    return Ok("Token is already revoked or expired.");
                }

                _tokenBlacklist.AddToBlacklist(token);

                return Ok("Logout successful");
            }

            return Ok("Logout successful");
        }



    }
}
