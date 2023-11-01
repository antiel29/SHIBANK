namespace SHIBANK.Helper
{
    public static class BankAccountHelper
    {
        public static string GenerateRandomCbu()
        {
            Random random = new Random();
            string chars = "0123456789";
            string cbu;

            cbu = new string(Enumerable.Repeat(chars, 22).
                Select(x => x[random.Next(x.Length)]).ToArray());

            return cbu;
        }
    }
}
