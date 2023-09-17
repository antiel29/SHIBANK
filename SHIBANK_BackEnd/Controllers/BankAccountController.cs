using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;
using SHIBANK.Services;

namespace SHIBANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public BankAccountController(IUserRepository userRepository, IBankAccountService bankAccountService, IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountService = bankAccountService;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccount>))]
        public IActionResult GetBankAccount()
        {
            var bankAccounts = _mapper.Map<List<BankAccountDto>>(_bankAccountRepository.GetBankAccounts());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bankAccounts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccount))]
        [ProducesResponseType(400)]
        public IActionResult GetBankAccount(int id)
        {
            if (!_bankAccountRepository.BankAccountExists(id))
            {
                return NotFound();
            }
            var bankAccount = _mapper.Map<BankAccountDto>(_bankAccountRepository.GetBankAccount(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(bankAccount);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccount>))]
        [ProducesResponseType(400)]
        public IActionResult GetBankAccountsByUser(int userId)
        {
            var bankAccounts = _mapper.Map<List<BankAccountDto>>(_bankAccountRepository.GetBankAccountsByUser(userId));

            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(bankAccounts);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBankAccount([FromBody] BankAccountCreateDto bankAccountCreate)
        {
            if (bankAccountCreate == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUser(bankAccountCreate.UserId);

            if (user == null)
            {
                ModelState.AddModelError("", "User doesn't exist");
                return StatusCode(422, ModelState);
            }

            var bankAccount = _bankAccountService.CreateBankAccountForUser(user.Id);

            return Ok("Successfully created");
        }

    }
}
