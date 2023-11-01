using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Security;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace SHIBANK.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [ProducesResponseType(200,Type = typeof(AuthResult))]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [HttpPost("login")]
        [SwaggerRequestExample(typeof(UserLoginDto), typeof(UserLoginDtoExample))]
        [SwaggerOperation(Summary = "Login endpoint", Description = "This endpoint allows you to log in, and upon success, it will return a **token**. \n\n" +
            " **IMPORTANT** Please copy the token and place it in the **'Authorize'** section to perform other actions.\n\n" +
            " If you want to test the app, an example with admin credentials is already provided.\n\n")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userloginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authResult = await _authService.AuthenticateAsync(userloginDto.Username!, userloginDto.Password!);

            if (!authResult.Success)
                return Unauthorized(authResult);

            return Ok(authResult);
        }

        [ProducesResponseType(200)]
        [HttpPost("logout")]
        [Authorize]
        [SwaggerOperation(Summary = "Logout endpoint", Description = "This endpoint allows you to log out, and upon success, it will put the token in a **blacklist**. \n\n")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                if (_authService.IsTokenBlacklisted(token))
                {
                    return Ok("Token is already revoked or expired.");
                }

                _authService.AddToBlacklist(token);

                return Ok("Logout successful");
            }

            return Ok("You already logout");
        }
    }
}
