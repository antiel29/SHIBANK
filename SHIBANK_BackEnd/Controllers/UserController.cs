using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Data;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<User>))]
        public IActionResult GetUser() 
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }


        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            return Ok(user);
        }
        
        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            if (!_userRepository.UserExists(username))
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(username));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult RegisterUser([FromBody] UserRegisterDto userRegister) 
        {
            if(userRegister == null) 
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(u => u.Username.Trim().ToUpper() == userRegister.Username.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userRegister);

            if (!_userRepository.RegisterUser(userMap)) 
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Succefully created");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int Id, [FromBody] UserDto updateUser)
        {
            if (updateUser == null)
                return BadRequest(ModelState);

            if (Id != updateUser.Id)
                return BadRequest(ModelState);

            var existingUser = _userRepository.GetUser(Id);

            if (existingUser == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            existingUser.Username = updateUser.Username;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.Email = updateUser.Email;

            if (!_userRepository.UpdateUser(existingUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int Id) 
        {
            if (!_userRepository.UserExists(Id))
                return NotFound();

            var userToDelete = _userRepository.GetUser(Id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }

    }
}
