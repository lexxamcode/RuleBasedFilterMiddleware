using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Extensions;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Model.Rules.Base.ParameterComparison;
using RuleBasedFilterLibrary.Services;
using RuleBasedFilterLibrary.Services.DeepAnalysis;
using RuleBasedFilterLibrary.Services.RequestValidation;
using System.Web;

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
