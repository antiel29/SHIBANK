using SHIBANK.Enums;
using SHIBANK.Helper;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using SHIBANK.Security;

namespace SHIBANK.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public CardService(ICardRepository cardRepository,IBankAccountRepository bankAccountRepository)
        {
            _cardRepository = cardRepository;
            _bankAccountRepository = bankAccountRepository;
        }
        public bool CardExists(int id)
        {
            return _cardRepository.CardExists(id);
        }

        public bool CardExists(string cardNumber)
        {
            var hashedCardNumber = cardNumber.GetHashCode();
            return _cardRepository.CardExists(hashedCardNumber);
        }

        public (string cardNumber,string LastFourDigits) GenerateUniqueCardNumber()
        {
            string cardNumber = "";
            bool isUnique = false;

            while (!isUnique)
            {
                cardNumber = CardHelper.GenerateRandomCardNumber();
                isUnique = !CardExists(cardNumber);

            }
            var lastFourDigits = cardNumber.Substring(8);
            return (cardNumber, lastFourDigits);
        }

        public bool CreateCard(int id, CardType type)
        {
            var cardNumber = GenerateUniqueCardNumber();
            var card = new Card
            {
                Type = type,
                CardNumber = Hashing.CalculateHash(cardNumber.cardNumber),
                LastFourDigits = cardNumber.LastFourDigits,
                ExpirationDate = DateTime.UtcNow.AddYears(5),
                CVC = Hashing.CalculateHash(CardHelper.GenerateRandomCvc()),
                UserId = id
            };
            if(type == CardType.Debit)
            {
                var bankAccount = _bankAccountRepository.GetUserBankAccountOfType(id, BankAccountType.Checking);
                card.BankAccountId = bankAccount.Id;
            }
            if(type == CardType.Credit)
            {
                card.AmountSpentThisMonth = 0;
                card.Limit = 100000.0m;
            }
            return _cardRepository.CreateCard(card);
        }

        public Card GetCard(int id)
        {
            return _cardRepository.GetCard(id);
        }
        public IEnumerable<Card> GetCards()
        {
            return _cardRepository.GetCards();
        }

        public IEnumerable<Card> GetUserCards(int id)
        {
            return _cardRepository.GetUserCards(id);
        }

        public Card GetUserCardType(int id, CardType type)
        {
            return _cardRepository.GetUserCardType(id, type);
        }

        public bool DeleteCard(Card card)
        {
            return _cardRepository.DeleteCard(card);
        }
    }
}
