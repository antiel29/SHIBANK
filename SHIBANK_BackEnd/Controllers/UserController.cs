using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using SHIBANK.Data;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;
using System.Security.Claims;
=======
using SHIBANK.Dto;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Models;
>>>>>>> master

namespace SHIBANK.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        //Get all users
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<User>))]
        public IActionResult GetUser() 
        {
            var users = _userService.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(usersDto);
        }

        //Get user by id
        [HttpGet("id/{Id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int Id)
        {
            if (!_userService.UserExists(Id)) 
                return NotFound();

            var user = _userService.GetUser(Id);
            var userDto = _mapper.Map<UserDto>(user);

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(userDto);
        }
        
        //Get user by username
        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            if (!_userService.UserExists(username))
                return NotFound();

            var user = _userService.GetUser(username);
            var userDto = _mapper.Map<UserDto>(user);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDto);
        }


        //Create a new user
        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
                return BadRequest(ModelState);

            if (!_userService.UserExists(userRegisterDto.Username))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            var user = _mapper.Map<User>(userRegisterDto);
            var success = _userService.RegisterUser(user);

            if (!success) 
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Created($"api/user/{user.Id}", "Successfully registered");
        }

        //Update a existing user
        [HttpPut("update")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
<<<<<<< HEAD
        public IActionResult UpdateUser( [FromBody] UserDto updateUser)
        {
            if (updateUser == null)
                return BadRequest(ModelState);

            var existingUser = _userService.GetUser(updateUser.Username);

            if (existingUser == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            existingUser.Username = updateUser.Username;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.Email = updateUser.Email;

            if (!_userService.UpdateUser(existingUser))
=======
        public IActionResult UpdateUser( [FromBody] UserUpdateDto updateUserDto)
        {
            if (updateUserDto == null)
                return BadRequest(ModelState);

            var Id = UserHelper.GetUserIdFromClaim(User);

            if (!_userService.UserExists(Id))
                return NotFound();

            var user = _userService.GetUser(Id);

            if (!ModelState.IsValid)
                return BadRequest();

            user.Username = updateUserDto.Username;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;

            if (!_userService.UpdateUser(user))
>>>>>>> master
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //Delete a user
        [HttpDelete("delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteUser() 
        {
<<<<<<< HEAD
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
=======
            var Id = UserHelper.GetUserIdFromClaim(User);
>>>>>>> master

            if (!_userService.UserExists(Id))
                return NotFound();

<<<<<<< HEAD
            var userToDelete = _userService.GetUser(Id);
=======
            var user = _userService.GetUser(Id);
>>>>>>> master

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

<<<<<<< HEAD
            if (!_userService.DeleteUser(userToDelete))
=======
            if (!_userService.DeleteUser(user))
>>>>>>> master
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }

    }
}
