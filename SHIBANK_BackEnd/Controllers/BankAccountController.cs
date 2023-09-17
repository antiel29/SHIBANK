using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Repository;

namespace SHIBANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;
        public BankAccountController(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
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

        [HttpGet("user/{userId}/bank")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccount>))]
        [ProducesResponseType(400)]
        public IActionResult GetBankAccountsByUser(int userId)
        {
            var bankAccounts = _mapper.Map<List<BankAccountDto>>(_bankAccountRepository.GetBankAccountsByUser(userId));

            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(bankAccounts);
        }

    }
}
