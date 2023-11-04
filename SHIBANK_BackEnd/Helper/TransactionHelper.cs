namespace SHIBANK.Helper
{
    public class TransactionHelper
    {
        public static string GenerateRandomTransactionCode()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string transactionCode;

            transactionCode = new string(Enumerable.Repeat(chars, 30).
                Select(x => x[random.Next(x.Length)]).ToArray());

            return transactionCode;
        }
    }
}
