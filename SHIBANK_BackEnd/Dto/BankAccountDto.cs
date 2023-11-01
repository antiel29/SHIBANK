using System.ComponentModel.DataAnnotations;

namespace SHIBANK.Dto
{
    public class BankAccountDto
    {
        public int Id { get; set; }

        public string? CBU { get; set; }

        public decimal Balance { get; set; }

    }
}
