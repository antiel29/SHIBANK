using System.Security.Cryptography;
using System.Text;

namespace SHIBANK.Security
{
    public static class Hashing
    {
        public static string CalculateHash(string number)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedNumber = sha256.ComputeHash(Encoding.UTF8.GetBytes(number));
                return Convert.ToBase64String(hashedNumber);
            }
        }

        public static bool Verify(string number,string hashedNumber) 
        {
            string calculatedHash = CalculateHash(number);
            return calculatedHash == hashedNumber;
        }
    }
}
