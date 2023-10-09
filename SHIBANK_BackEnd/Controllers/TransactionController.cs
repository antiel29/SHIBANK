using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Services;

namespace SHIBANK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountService _bankAccountService;

        public TransactionController(IMapper mapper,ITransactionService transactionService, IBankAccountService bankAccountService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        public IActionResult GetTransaction()
        {
            var transactions = _transactionService.GetTransactions();
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400)]
        public IActionResult GetTransaction(int id)
        {
            if (!_transactionService.TransactionExists(id))
                return NotFound();

            var transaction = _transactionService.GetTransaction(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionDto);
        }

        [HttpGet("bank/{bankAccountId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
         public IActionResult GetTransactionsByBankAccount(int bankAccountId)
        {
            var transactions = _transactionService.GetTransactionsByBankAccount(bankAccountId);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid) 
             return BadRequest(ModelState);

            return Ok(transactionsDto);
        }

        [HttpGet("send/bank/{accountNumber}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionsByBankAccount(string accountNumber)
        {
            var transactions = _transactionService.GetTransactionsByBankAccount(accountNumber);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsDto);
        }

        
        [HttpGet("received/bank/{accountNumber}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionsRecieved(string accountNumber)
        {
            var transactions = _transactionService.GetTransactionsRecieved(accountNumber);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsDto);
        }

        [HttpGet("received/user/{username}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionsRecievedUsername(string username)
        {
            var transactions = _transactionService.GetTransactionsRecievedUsername(username);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsDto);
        }


        [HttpGet("user/{username}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]
        public IActionResult GetTransactionsByUsername(string username)
        {
            var transactions = _transactionService.GetTransactionsByBankAccount(username);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactionsDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction([FromBody] TransactionCreateDto transactionCreate)
        {
            if (transactionCreate == null)
                return BadRequest(ModelState);

            var bankAccountOrigin = _bankAccountService.GetBankAccount(transactionCreate.OriginAccountNumber);
            var bankAccountDestiny = _bankAccountService.GetBankAccount(transactionCreate.DestinyAccountNumber);

            if (bankAccountOrigin == null || bankAccountDestiny == null)
            {
                ModelState.AddModelError("", "Bank account doesn't exist");
                return StatusCode(422, ModelState);
            }

            if (transactionCreate.OriginAccountNumber == transactionCreate.DestinyAccountNumber)
            {
                ModelState.AddModelError("", "Can't transfer the same account");
                return StatusCode(422, ModelState);
            }

            if (bankAccountOrigin.Balance - transactionCreate.Amount < 0)
            {
                ModelState.AddModelError("", "Insuficient founds");
                return StatusCode(422, ModelState);
            }

            var transaction = _transactionService.CreateTransactionOD(transactionCreate, bankAccountOrigin, bankAccountDestiny);

            _transactionService.CreateTransaction(transaction);

            return NoContent();
        }

    }
}
