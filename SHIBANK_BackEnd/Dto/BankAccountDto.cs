using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class BankAccountDto
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

    }
}
