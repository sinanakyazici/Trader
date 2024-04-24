using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BuildingBlocks.Core.Middleware;

public class FakeIdentityMiddleware
{
    private readonly RequestDelegate _next;

    public FakeIdentityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("preferred_username", "sinan.akyazici") }));
        await _next(context);
    }
}