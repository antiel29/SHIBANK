namespace SHIBANK.Interfaces
{
    public interface ITokenBlacklistService
    {
        void AddToBlacklist(string token);

        bool IsTokenBlacklisted(string token);
    }
}
