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
    [Authorize]
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
        public IActionResult CreateBankAccount([FromQuery] BankAccountCreateDto bankAccountCreate)
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



        [HttpPut("deposit/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Deposit(int id, [FromQuery] decimal amount)
        {
            if (amount < 0)
                return BadRequest(ModelState);

            var existingBankAccount = _bankAccountRepository.GetBankAccount(id);

            if (existingBankAccount == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingBankAccount.Balance += amount;

            if (!_bankAccountRepository.Deposit(existingBankAccount))
            {
                ModelState.AddModelError("", "Something went wrong trying deposit");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("withdraw/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Withdraw(int id, [FromQuery] decimal amount)
        {
            if (amount < 0)
                return BadRequest(ModelState);

            var existingBankAccount = _bankAccountRepository.GetBankAccount(id);

            if (existingBankAccount == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(existingBankAccount.Balance < amount)
                return BadRequest(ModelState);

            existingBankAccount.Balance -= amount;

            if (!_bankAccountRepository.Withdraw(existingBankAccount))
            {
                ModelState.AddModelError("", "Something went wrong trying withdraw");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBankAccount(int Id)
        {
            if (!_bankAccountRepository.BankAccountExists(Id))
                return NotFound();

            var bankAccountToDelete = _bankAccountRepository.GetBankAccount(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankAccountRepository.DeleteBankAccount(bankAccountToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bank account");
            }
            return NoContent();
        }
    }
}
