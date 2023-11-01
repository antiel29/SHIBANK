using SHIBANK.Security;

public class TokenBlacklist
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
        CleanupExpiredTokens();

        return _blacklistedTokens.Any(t => t.Token == token);
    }

    private void CleanupExpiredTokens()
    {
        var now = DateTime.UtcNow;
        _blacklistedTokens.RemoveAll(t => t.ExpiryDate < now);
    }
}
