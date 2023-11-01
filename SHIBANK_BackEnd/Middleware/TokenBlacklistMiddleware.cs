using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SHIBANK.Interfaces;

public class TokenBlacklistMiddleware : IMiddleware
{
    private readonly TokenBlacklist _tokenBlacklist;

    public TokenBlacklistMiddleware(TokenBlacklist tokenBlacklist)
    {
        _tokenBlacklist = tokenBlacklist;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && _tokenBlacklist.IsTokenBlacklisted(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token is blacklisted or expired.");
            return;
        }

        await next(context);
    }
}