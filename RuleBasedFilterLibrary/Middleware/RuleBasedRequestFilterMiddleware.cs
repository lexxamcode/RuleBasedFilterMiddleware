using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Middleware.Services.RequestValidation;

namespace RuleBasedFilterLibrary.Middleware;

public class RuleBasedRequestFilterMiddleware(
    RequestDelegate next,
    IRequestValidationService requestValidationService)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var isRequestValid = await requestValidationService.IsValidAsync(context);

        if (!isRequestValid)
        {
            context.Response.StatusCode = 403;
            return;
        }

        await _next(context);
    }
}
