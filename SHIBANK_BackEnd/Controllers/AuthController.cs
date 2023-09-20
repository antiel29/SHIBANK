using Microsoft.AspNetCore.Authentication;
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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //Login
        [HttpPost]
        public IActionResult Login([FromBody]UserLoginDto userloginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _authService.Authenticate(userloginDto.Username, userloginDto.Password);

            if (user == null)
                return Unauthorized();

            var token = _authService.GenerateToken(user);

            return Ok(new { token });
        }



    }
}
