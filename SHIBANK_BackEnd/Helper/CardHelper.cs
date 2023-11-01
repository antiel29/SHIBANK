namespace SHIBANK.Helper
{
    public static class CardHelper
    {
        public static string GenerateRandomCardNumber()
        {
            Random random = new Random();
            string chars = "0123456789";
            string cardNumber;

            cardNumber = new string(Enumerable.Repeat(chars,12).
                Select(x => x[random.Next(x.Length)]).ToArray());

            return cardNumber;
        }

        public static string GenerateRandomCvc()
        {
            Random random = new Random();
            string chars = "0123456789";
            string cvc;

            cvc = new string(Enumerable.Repeat(chars, 3).
                Select(x => x[random.Next(x.Length)]).ToArray());

            return cvc;
        }
        
    }
}
