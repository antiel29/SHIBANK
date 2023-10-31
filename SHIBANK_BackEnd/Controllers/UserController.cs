using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace SHIBANK.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<UserDto>))]
        [SwaggerOperation(Summary = "Get a list of all users",Description = "This endpoint returns a list of all users.")]
        public IActionResult GetUsers() 
        {
            var users = _userService.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get user by id", Description = "Retrieve user information by their unique id.")]

        public IActionResult GetUser(int id)
        {
            if (!_userService.UserExists(id)) 
                return NotFound();

            var user = _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
        
        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get user by username", Description = "Retrieve user information by their unique username.")]
        public IActionResult GetUser(string username)
        {
            if (!_userService.UserExists(username))
                return NotFound();

            var user = _userService.GetUser(username);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("current")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(401)]
        [Authorize]
        [SwaggerOperation(Summary = "Get current user", Description = "Retrieve user information for the currently authenticated user.")]
        public IActionResult GetUser()
        {
            var id = UserHelper.GetUserIdFromClaim(User);

            if (!_userService.UserExists(id))
                return NotFound();

            var user = _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerRequestExample(typeof(UserRegisterDto), typeof(UserRegisterDtoExample))]
        [SwaggerOperation(Summary = "Create a new user",
            Description = "This endpoint allows you to create a new user in the system. " +
            "To successfully register, ensure the following criteria are met:\n " +
            "- Username must have a minimum length of 3 characters,maximum of 50 and be unique.\n " +
            "- Password must have a minimum length of 8 characters,maximum of 50 and include at least one digit,one lowercase letter, and one uppercase letter.\n" +
            "- First name cannot exceed 50 characters.\n" +
            "- Last name cannot exceed 50 characters.\n" +
            "- Email cannot exceed 100 characters address and must be valid.\n ")]
        public IActionResult RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data. Please check the provided information.");
                return BadRequest();
            }

            if (_userService.UserExists(userRegisterDto.Username!))
            {
                ModelState.AddModelError("", "Username already exists. Please choose a different username.");
                return StatusCode(422, ModelState);
            }

            var user = _mapper.Map<User>(userRegisterDto);

            var success = _userService.RegisterUser(user,userRegisterDto.Password!);

            if (!success.Result) 
            {
                ModelState.AddModelError("", "Error while attempting to save the user in the database.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("current/update")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        [SwaggerOperation(Summary = "Update current user",
            Description = "This endpoint allows you to update some or all of the field of the current user.\n" +
            "To avoid updating a specific field, simply omit it from your request body.\n" +
            "- Username must have a minimum length of 3 characters,maximum of 50 and be unique.\n" +
            "- First name cannot exceed 50 characters.\n" +
            "- Last name cannot exceed 50 characters.\n" +
            "- Email cannot exceed 100 characters address and must be valid.")]

        public IActionResult UpdateUser( [FromBody] UserUpdateDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data. Please check the provided information.");
                return BadRequest();
            }

            var id = UserHelper.GetUserIdFromClaim(User);
            var user = _userService.GetUser(id);

            if (updateUserDto.Username != null &&
                updateUserDto.Username != user.UserName &&
                _userService.UserExists(updateUserDto.Username))
            {
                ModelState.AddModelError("", "Username already exists. Please choose a different username.");
                return StatusCode(422, ModelState);
            }

            user.UserName = updateUserDto.Username ?? user.UserName;
            user.FirstName = updateUserDto.FirstName ?? user.FirstName;
            user.LastName = updateUserDto.LastName ?? user.LastName;
            user.Email = updateUserDto.Email ?? user.Email;

            if (!_userService.UpdateUser(user))
            {
                ModelState.AddModelError("", "Error while attempting to update user.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("{id}/update")]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "admin")]
        [SwaggerOperation(Summary = "Update user by id (admin)",
            Description = "To avoid updating a specific field, simply omit it from your request body.\n" +
            "- Username must have a minimum length of 3 characters,maximum of 50 and be unique.\n" +
            "- First name cannot exceed 50 characters.\n" +
            "- Last name cannot exceed 50 characters.\n" +
            "- Email cannot exceed 100 characters address and must be valid.")]

        public IActionResult UpdateUser(int id, [FromBody] UserUpdateDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data. Please check the provided information.");
                return BadRequest();
            }

            if (!_userService.UserExists(id))
                return NotFound();

            var user = _userService.GetUser(id);

            if (updateUserDto.Username != null &&
                updateUserDto.Username != user.UserName &&
                _userService.UserExists(updateUserDto.Username))
            {
                ModelState.AddModelError("", "Username already exists. Please choose a different username.");
                return StatusCode(422, ModelState);
            }

            user.UserName = updateUserDto.Username ?? user.UserName;
            user.FirstName = updateUserDto.FirstName ?? user.FirstName;
            user.LastName = updateUserDto.LastName ?? user.LastName;
            user.Email = updateUserDto.Email ?? user.Email;

            if (!_userService.UpdateUser(user))
            {
                ModelState.AddModelError("", "Error while attempting to update user.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("current/changepassword")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [SwaggerOperation(Summary = "Change current user password",
            Description = "This endpoint allows you to change your current password(needs your old password).\n" +
            "- New password must have a minimum length of 8 characters,maximum of 50 and include at least one digit,one lowercase letter, and one uppercase letter.")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input data. Please check the provided information.");
                return BadRequest();
            }

            var id = UserHelper.GetUserIdFromClaim(User);
            var user = _userService.GetUser(id);

            var success = _userService.ChangePassword(user, changePasswordDto.oldPassword!, changePasswordDto.newPassword!);

            if (!success.Result)
            {
                ModelState.AddModelError("", "Error while attempting to change password,verify if the old password is correct.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("current/delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        [SwaggerOperation(Summary = "Delete current user",Description = "Delete current user with all of his information(accounts and transactions)")]
        public IActionResult DeleteUser() 
        {
            var id = UserHelper.GetUserIdFromClaim(User);

            if (!_userService.UserExists(id))
                return NotFound();

            var user = _userService.GetUser(id);

            if (!_userService.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles ="admin")]
        [SwaggerOperation(Summary = "Delete user by id (admin)", Description = "Delete user with all of his information(accounts and transactions)")]
        public IActionResult DeleteUser(int id)
        {
            if (!_userService.UserExists(id))
                return NotFound();

            var user = _userService.GetUser(id);

            if (!_userService.DeleteUser(user))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }

    }
}
