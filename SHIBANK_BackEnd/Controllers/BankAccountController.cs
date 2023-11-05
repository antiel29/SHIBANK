using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Enums;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace SHIBANK.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public BankAccountController( IBankAccountService bankAccountService, IMapper mapper, IUserService userService)
        {
            _bankAccountService = bankAccountService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccountDto>))]
        [SwaggerOperation(Summary = "Get a list of all bank accounts (admin)", Description = "This endpoint returns a list of all bank accounts.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetBankAccounts()
        {
            var bankAccounts = _bankAccountService.GetBankAccounts();
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccountDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get bank account by id (admin)", Description = "Retrieve bank account information by their unique id.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetBankAccount(int id)
        {
            if (!_bankAccountService.BankAccountExists(id))
                return NotFound();

            var bankAccount = _bankAccountService.GetBankAccount(id);
            var bankAccountDto = _mapper.Map<BankAccountDto>(bankAccount);

            return Ok(bankAccountDto);
        }

        [HttpGet("cbu/{cbu}")]
        [ProducesResponseType(200, Type = typeof(BankAccountDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get bank account by cbu (admin)", Description = "Retrieve bank account information by their unique cbu.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetBankAccount(string cbu)
        {
            if (!_bankAccountService.BankAccountExists(cbu))
                return NotFound();

            var bankAccount = _bankAccountService.GetBankAccount(cbu);
            var bankAccountDto = _mapper.Map<BankAccountDto>(bankAccount);

            return Ok(bankAccountDto);
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccountDto>))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get bank account by user id (admin)", Description = "Retrieve bank account information by their user id.")]
        [Authorize(Roles = "admin")]

        public IActionResult GetBankAccountsByUser(int id)
        {
            if (!_userService.UserExists(id))
                return NotFound(); 

            var bankAccounts = _bankAccountService.GetBankAccountsByUser(id);
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        [HttpGet("current")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccountDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get current logged user bank accounts", Description = "Retrieve bank accounts information for the currently authenticated user.")]
        [Authorize]
        public IActionResult GetUserBankAccounts()
        {
            var id = UserHelper.GetUserIdFromClaim(User);

            var bankAccounts = _bankAccountService.GetBankAccountsByUser(id);
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            return Ok(bankAccountsDto);
        }


        [HttpPost("current/create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Create a new account",
            Description = "This endpoint allows you to create a new account in the system.\n\n" +
            "**If no type is selected, a checking account will be created by default.**\n\n " +
            "The users are limited to owning one account per type.\n\n " +
            "Savings accounts generate a 2% interest rate every 30 days and have a limit of 5 transactions within the same 30-day period.\n\n")]
        [Authorize]
        public IActionResult CreateBankAccount(BankAccountType type)
        {
            var userId = UserHelper.GetUserIdFromClaim(User);

            if (_bankAccountService.GetUserBankAccountOfType(userId,type) != null)
            {
                ModelState.AddModelError("", $"Currently the limit of {type} accounts is 1");
                return BadRequest(ModelState);
            }

            if (!_bankAccountService.CreateBankAccount(userId, type))
            {
                ModelState.AddModelError("", "Error while attempting to create the account in the database.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpPut("current/move-funds")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Move funds between personal accounts",
            Description = "This endpoint allows you to move a certain amount between your accounts.\n\n" +
            "The minimum transfer amount is $100.\n\n" +
            "Must specify the source and destination accounts for the transfer.\n\n" +
            "Transferring funds to or from a savings account will be considered as one transaction.\n\n ")]
        [Authorize]
        public IActionResult MoveFunds(BankAccountType source,BankAccountType destiny,[FromQuery] decimal amount)
        {
            if (amount < 100 || source == destiny)
                return BadRequest(ModelState);

            var id = UserHelper.GetUserIdFromClaim(User);

            var sourceAccount = _bankAccountService.GetUserBankAccountOfType(id, source);
            var destinyAccount = _bankAccountService.GetUserBankAccountOfType(id, destiny);

            if (sourceAccount == null || destinyAccount == null)
                return NotFound();

            Result result = _bankAccountService.MoveFunds(sourceAccount, destinyAccount, amount);

            if (!result.Success)
            {
                ModelState.AddModelError("", $"{result.Information}");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpDelete("current/{type}/delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Delete selected current user logged bank account", Description = "Delete bank account with **all** of his transactions")]
        [Authorize]
        public IActionResult DeleteBankAccount(BankAccountType type)
        {
            var userId = UserHelper.GetUserIdFromClaim(User);

            var bankAccountToDelete = _bankAccountService.GetUserBankAccountOfType(userId, type);

            if (bankAccountToDelete == null)
                return NotFound();

            if (!_bankAccountService.DeleteBankAccount(bankAccountToDelete!))
            {
                ModelState.AddModelError("", "Something went wrong deleting bank account");
            }
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Delete bank account by id (admin)", Description = "Delete bank account with **all** of his transactions")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteBankAccount(int id)
        {
            if (!_bankAccountService.BankAccountExists(id))
                return NotFound();

            var bankAccountToDelete = _bankAccountService.GetBankAccount(id);

            if (!_bankAccountService.DeleteBankAccount(bankAccountToDelete!))
            {
                ModelState.AddModelError("", "Something went wrong deleting bank account");
            }
            return NoContent();
        }
    }
}
