using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHIBANK.Dto;
using SHIBANK.Enums;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Repository;
using SHIBANK.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SHIBANK.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;

        public CardController (ICardService cardService, IMapper mapper, IBankAccountService bankAccountService)
        {
            _cardService = cardService;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get all cards (admin)",Description = "This endpoint returns a list of all cards.")]
        [Authorize(Roles ="admin")]
        public IActionResult GetCards()
        {
            var cards = _cardService.GetCards();
            var cardsDto = _mapper.Map<List<CardDto>>(cards);

            return Ok(cardsDto);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get card by id (admin)", Description = "This endpoint return a card by id.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetCard(int id)
        {
            if (!_cardService.CardExists(id))
                return NotFound();

            var card = _cardService.GetCard(id);
            var cardDto = _mapper.Map<CardDto>(card);

            return Ok(cardDto);
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get user cards by his id", Description = "This endpoint return user cards.")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUserCards(int id)
        {
            if (!_cardService.CardExists(id))
                return NotFound();

            var cards = _cardService.GetUserCards(id);
            var cardsDto = _mapper.Map<List<CardDto>>(cards);

            return Ok(cardsDto);
        }

        [HttpGet("current")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDto>))]
        [ProducesResponseType(401)]
        [SwaggerOperation(Summary = "Get current user cards", Description = "This endpoint return current user cards.")]
        [Authorize]
        public IActionResult GetCurrentCards()
        {
            var id = UserHelper.GetUserIdFromClaim(User);

            var cards = _cardService.GetUserCards(id);
            var cardsDto = _mapper.Map<List<CardDto>>(cards);

            return Ok(cardsDto);
        }

        
        [HttpPost("generate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [SwaggerOperation(Summary = "Generate a card", 
            Description = "This endpoint allows you to create a new card, which can be either a debit card or a credit card.\n\n" +
            "Debit cards **require** an associated checking account.\n\n" +
            "Credit cards come with a $100,000 monthly spending limit.")]
        [Authorize]
        public IActionResult CreateCard(CardType type)
        {
            var id = UserHelper.GetUserIdFromClaim(User);

            if (_cardService.GetUserCardType(id,type) != null)
            {
                ModelState.AddModelError("", $"You already have a {type} card.");
                return BadRequest(ModelState);
            }
            if (_bankAccountService.GetUserBankAccountOfType(id, BankAccountType.Checking) == null)
            {
                ModelState.AddModelError("", $"You need a cheking account if you want to have a {type} card.");
                return BadRequest(ModelState);
            }
            if (!_cardService.CreateCard(id, type))
            {
                ModelState.AddModelError("", "Error while attempting to create a card.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("delete")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Delete selected card",
            Description = "This endpoint allows you to delete a card.\n\n" +
            "For credit cards, the card must not have a balance or outstanding payments for the current month in order to be deleted.\n\n")]
        [Authorize]
        public IActionResult DeleteCard(CardType type)
        {
            var id = UserHelper.GetUserIdFromClaim(User);
            var card = _cardService.GetUserCardType(id, type);

            if (card == null)
            {
                ModelState.AddModelError("", $"You don´t have a {type} card.");
                return BadRequest(ModelState);
            }
            if (card.AmountSpentThisMonth != null && card.AmountSpentThisMonth > 0)
            {
                ModelState.AddModelError("", $"The {type} card must not have a balance or outstanding payments for the current month in order to be deleted.");
                return BadRequest(ModelState);
            }

            if (!_cardService.DeleteCard(card))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {type} card");
            }
            return NoContent();
        }


    }

}
