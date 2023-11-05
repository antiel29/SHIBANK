using SHIBANK.Enums;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface ICardService
    {
        IEnumerable<Card> GetCards();
        bool CardExists(int id);
        bool CardExists(string cardNumber);
        Card? GetCard(int id);
        Card? GetUserCardType(int id, CardType type);

        (string cardNumber, string LastFourDigits) GenerateUniqueCardNumber();

        bool DeleteCard(Card card);
        bool CreateCard(int id,CardType type);
        IEnumerable<Card> GetUserCards(int id);
    }
}
