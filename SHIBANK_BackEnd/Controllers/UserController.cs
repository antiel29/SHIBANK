using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Data;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;
using System.Security.Claims;

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
            var Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!_userService.UserExists(Id))
                return NotFound();

            var userToDelete = _userService.GetUser(Id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }

    }
}
