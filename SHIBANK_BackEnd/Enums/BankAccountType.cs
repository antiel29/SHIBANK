using System.Text.Json.Serialization;

namespace SHIBANK.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BankAccountType
    {
        Checking,
        Savings
    }
}
