using SHIBANK.Interfaces;
namespace SHIBANK.Services;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly List<TokenInfo> _blacklistedTokens = new List<TokenInfo>();
    private readonly TimeSpan _tokenLifetime = TimeSpan.FromDays(1);

    public void AddToBlacklist(string token)
    {
        _blacklistedTokens.Add(new TokenInfo
        {
            Token = token,
            ExpiryDate = DateTime.UtcNow.Add(_tokenLifetime)
        });
    }

    public bool IsTokenBlacklisted(string token)
    {
        return _blacklistedTokens.Any(t => t.Token == token);
    }
}

public class TokenInfo
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
}
