﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SHIBANK.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        public BankAccountController( IBankAccountService bankAccountService, IMapper mapper)
        {
            _bankAccountService = bankAccountService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccountDto>))]
        [SwaggerOperation(Summary = "Get a list of all bank accounts", Description = "This endpoint returns a list of all bank accounts.")]
        public IActionResult GetBankAccount()
        {
            var bankAccounts = _bankAccountService.GetBankAccounts();
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BankAccountDto))]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Get bank account by id", Description = "Retrieve bank account information by their unique id.")]
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
        [SwaggerOperation(Summary = "Get bank account by cbu", Description = "Retrieve bank account information by their unique cbu.")]
        public IActionResult GetBankAccount(string cbu)
        {
            if (!_bankAccountService.BankAccountExists(cbu))
                return NotFound();

            var bankAccount = _bankAccountService.GetBankAccount(cbu);
            var bankAccountDto = _mapper.Map<BankAccountDto>(bankAccount);

            return Ok(bankAccountDto);
        }

        //Get user all bank accounts
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccountDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetBankAccountsByUser(int userId)
        {
            var bankAccounts = _bankAccountService.GetBankAccountsByUser(userId);
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            return Ok(bankAccountsDto);
        }

        //Get actual user bank accounts
        [HttpGet("user")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BankAccount>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserBankAccounts()
        {
            var userId = UserHelper.GetUserIdFromClaim(User);

            var bankAccounts = _bankAccountService.GetBankAccountsByUser(userId);
            var bankAccountsDto = _mapper.Map<List<BankAccountDto>>(bankAccounts);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bankAccountsDto);
        }


        //Create actual user a new bank account
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBankAccount()
        {

            var userId = UserHelper.GetUserIdFromClaim(User);
            var userBankAccounts = _bankAccountService.GetBankAccountsByUser(userId);

            if (userBankAccounts.Count() >= 5)
            {
                ModelState.AddModelError("", "The limit of bank account is 5");
                return BadRequest(ModelState);
            }

            var bankAccount = _bankAccountService.CreateBankAccountForUser(userId);

            return NoContent();
        }


        //Deposit in an accountNumber
        [HttpPut("deposit/{accountNumber}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Deposit(string accountNumber,[FromQuery] decimal amount)
        {
            if (amount < 0)
                return BadRequest(ModelState);

            var existingBankAccount = _bankAccountService.GetBankAccount(accountNumber);

            if (existingBankAccount == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingBankAccount.Balance += amount;

            if (!_bankAccountService.Deposit(existingBankAccount))
            {
                ModelState.AddModelError("", "Something went wrong trying deposit");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //Withdraw in an accountNumber
        [HttpPut("withdraw/{accountNumber}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Withdraw(string accountNumber, [FromQuery] decimal amount)
        {
            if (amount < 0)
                return BadRequest(ModelState);

            var existingBankAccount = _bankAccountService.GetBankAccount(accountNumber);

            if (existingBankAccount == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(existingBankAccount.Balance < amount)
                return BadRequest(ModelState);

            existingBankAccount.Balance -= amount;

            if (!_bankAccountService.Withdraw(existingBankAccount))
            {
                ModelState.AddModelError("", "Something went wrong trying withdraw");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //Delete a bank account by id
        [HttpDelete("admin/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBankAccount(int Id)
        {
            if (!_bankAccountService.BankAccountExists(Id))
                return NotFound();

            var bankAccountToDelete = _bankAccountService.GetBankAccount(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bankAccountService.DeleteBankAccount(bankAccountToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bank account");
            }
            return NoContent();
        }

        //Delete a bank account by numberAccount
        [HttpDelete("{numberAccount}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBankAccount(string numberAccount)
        {
            if (!_bankAccountService.BankAccountExists(numberAccount))
                return NotFound();

            var bankAccountToDelete = _bankAccountService.GetBankAccount(numberAccount);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = UserHelper.GetUserIdFromClaim(User);

            if (bankAccountToDelete.UserId != userId)
            {
                ModelState.AddModelError("", "You don't have a bank account with this number, please try again.");
                return BadRequest(ModelState);
            }

            if (!_bankAccountService.DeleteBankAccount(bankAccountToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bank account");
            }
            return NoContent();
        }

    }
}
