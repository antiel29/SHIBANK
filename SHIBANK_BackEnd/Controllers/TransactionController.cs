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
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository , IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
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

    }
}
