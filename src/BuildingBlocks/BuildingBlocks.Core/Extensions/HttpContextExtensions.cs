using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Core.Extensions;

public static class HttpContextExtensions
{
    public static string GetCurrentUserId(this HttpContext httpContext)
    {
        var value = httpContext?.User?.FindFirst("preferred_username")?.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            value = httpContext?.User?.FindFirst("client_id")?.Value;
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            value = "unknown";
        }

        return value;
    }
}