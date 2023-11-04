using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Enums;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SHIBANK.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IUserService _userService;

        public TransactionController(IMapper mapper,ITransactionService transactionService, IBankAccountService bankAccountService, IUserService userService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [SwaggerOperation(Summary = "Get a list of all transactions", Description = "This endpoint returns a list of all transactions.")]
        [Authorize]
        public IActionResult GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TransactionDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get transaction by id (admin)", Description = "Retrieve transaction information by their unique id.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetTransaction(int id)
        {
            if (!_transactionService.TransactionExists(id))
                return NotFound();

            var transaction = _transactionService.GetTransaction(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionDto);
        }

        [HttpGet("current/recieved")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get current user recieved transactions", Description = "Retrieve all recieved transactions.")]
        [Authorize]
        public IActionResult GetRecievedTransactions()
        {
            var userId = UserHelper.GetUserIdFromClaim(User);
            var user = _userService.GetUser(userId);

            var transactions = _transactionService.GetUserRecievedTransactions(user);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("current/sended")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get current user sended transactions", Description = "Retrieve all sended transactions.")]
        [Authorize]
        public IActionResult GetSendedTransactions()
        {
            var userId = UserHelper.GetUserIdFromClaim(User);
            var user = _userService.GetUser(userId);

            var transactions = _transactionService.GetUserSendedTransactions(user);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }

        [HttpGet("current")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransactionDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get current user transactions", Description = "Retrieve all transactions.")]
        [Authorize]
        public IActionResult GetAllTransactions()
        {
            var userId = UserHelper.GetUserIdFromClaim(User);
            var user = _userService.GetUser(userId);

            var transactions = _transactionService.GetUserAllTransactions(user);
            var transactionsDto = _mapper.Map<List<TransactionDto>>(transactions);

            return Ok(transactionsDto);
        }

        [HttpPost("current/make")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Make a transaction",
            Description = "This endpoint allows you to make a transaction.\n\n" +
            "The system will automatically select the current user checking account and the destination user checking account.\n\n" +
            "**Currently, we just admit transactions between chekings accounts**\n\n" +
            "Message it's opcional")]
        [Authorize]
        public IActionResult CreateTransaction([FromBody] TransactionCreateDto transactionCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userService.UserExists(transactionCreateDto.Username!))
                return NotFound();

            var destinyUser = _userService.GetUser(transactionCreateDto.Username!);
            var sourceId = UserHelper.GetUserIdFromClaim(User);

            if (destinyUser.Id == sourceId)
                return BadRequest(ModelState);

            var sourceAccount = _bankAccountService.GetUserBankAccountOfType(sourceId, BankAccountType.Checking);
            var destinyAccount = _bankAccountService.GetUserBankAccountOfType(destinyUser, BankAccountType.Checking);

            if (sourceAccount == null || destinyAccount == null)
                return NotFound();

            if (sourceAccount.Balance < transactionCreateDto.Amount)
            {
                ModelState.AddModelError("", "Insufficient funds.");
                return BadRequest(ModelState);
            }

            if (!_transactionService.CreateTransaction(sourceAccount, destinyAccount, transactionCreateDto))
            {
                ModelState.AddModelError("", "Error while attempting to transfer.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Delete transaction by id (admin)", Description = "Delete transaction by id.")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteTransaction(int id)
        {
            if (!_transactionService.TransactionExists(id))
                return NotFound();

            var trasaction = _transactionService.GetTransaction(id);

            if (!_transactionService.DeleteTransaction(trasaction))
            {
                ModelState.AddModelError("", "Something went wrong deleting transaction.");
            }
            return NoContent();
        }
    }
}
