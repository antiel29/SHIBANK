using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SHIBANK.Interfaces;

public class TokenBlacklistMiddleware : IMiddleware
{
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public TokenBlacklistMiddleware(ITokenBlacklistService tokenBlacklistService)
    {
        _tokenBlacklistService = tokenBlacklistService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null && _tokenBlacklistService.IsTokenBlacklisted(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token is blacklisted or expired.");
            return;
        }

        await next(context);
    }
}