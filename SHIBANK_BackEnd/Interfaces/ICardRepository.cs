using SHIBANK.Enums;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ICardRepository
    {
        ICollection<Card> GetCards();

        bool DeleteCard(Card card);
        bool CardExists(int id);
        bool CardExists(string cardNumber);
        Card? GetCard(int id);

        Card? GetUserCardType(int id,CardType type);

        bool CreateCard(Card card);
        ICollection<Card> GetUserCards(int id);

        bool Save();
    }
}
