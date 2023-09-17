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
        


    }
}
