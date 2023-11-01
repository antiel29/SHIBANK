namespace SHIBANK.Security
{
    public class AuthResult
    {
        public AuthResult(bool success, string token, string information)
        {
            Success = success;
            Token = token;
            Information = information;
        }
        public bool Success { get; }
        public string Token { get; }
        public string Information { get; } = string.Empty;

    }
}
