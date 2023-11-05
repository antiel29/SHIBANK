using SHIBANK.Data;
using SHIBANK.Enums;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;

        public CardRepository(DataContext context)
        {
            _context = context;
        }
        public bool CardExists(int id)
        {
            return _context.Cards.Any(c => c.Id == id);
        }

        public bool CardExists(string cardNumber)
        {
            return _context.Cards.Any(c => c.CardNumber == cardNumber);
        }

        public bool CreateCard(Card card)
        {
            _context.Add(card);
            return Save();
        }

        public bool DeleteCard(Card card)
        {
            _context.Remove(card);
            return Save();
        }

        public Card? GetCard(int id)
        {
            return _context.Cards.Where(c => c.Id == id).FirstOrDefault();
        }
        public ICollection<Card> GetCards()
        {
            return _context.Cards.OrderBy(c => c.Id).ToList();
        }

        public ICollection<Card> GetUserCards(int id)
        {
            return _context.Cards.Where(c => c.UserId == id).ToList();
        }

        public Card? GetUserCardType(int id, CardType type)
        {
            return _context.Cards.Where(c => c.UserId == id && c.Type == type).FirstOrDefault();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
