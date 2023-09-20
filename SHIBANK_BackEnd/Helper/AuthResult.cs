namespace SHIBANK.Helper
{
    public class AuthResult
    {
        public bool Success { get; }
        public string Token { get; }

        public AuthResult(bool success, string token)
        {
            Success = success;
            Token = token;
        }
    }
}
