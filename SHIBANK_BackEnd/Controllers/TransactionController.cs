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
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionRepository transactionRepository , IMapper mapper, IBankAccountRepository bankAccountRepository, ITransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _mapper = mapper;
            _bankAccountRepository = bankAccountRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        public IActionResult GetTransaction()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionRepository.GetTransactions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400)]
        public IActionResult GetTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
            {
                return NotFound();
            }
            var transaction = _mapper.Map<TransactionDto>(_transactionRepository.GetTransaction(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(transaction);
        }

        [HttpGet("bank/{bankAccountId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
         public IActionResult GetTransactionsByBankAccount(int bankAccountId)
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionRepository.GetTransactionsByBankAccount(bankAccountId));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(transactions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction([FromBody] TransactionCreateDto transactionCreate,int IdDestination )
        {
            if (transactionCreate == null)
                return BadRequest(ModelState);

            var bankAccountOrigin = _bankAccountRepository.GetBankAccount(transactionCreate.BankAccountId);
            var bankAccountDestination = _bankAccountRepository.GetBankAccount(IdDestination);

            if (bankAccountOrigin == null || bankAccountDestination == null)
            {
                ModelState.AddModelError("", "Bank account doesn't exist");
                return StatusCode(422, ModelState);
            }

            if (bankAccountOrigin.Balance - transactionCreate.Amount < 0)
            {
                ModelState.AddModelError("", "Insuficient founds");
                return StatusCode(422, ModelState);
            }

            var transaction = _transactionService.CreateTransactionOD(transactionCreate,bankAccountOrigin, bankAccountDestination);

            _transactionRepository.CreateTransaction(transaction);

            return Ok("Successfully created");
        }

    }
}
