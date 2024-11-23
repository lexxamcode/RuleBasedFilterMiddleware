using Microsoft.AspNetCore.Http;
using RuleBasedFilterLibrary.Model.Requests;
using RuleBasedFilterLibrary.Model.Rules.Base;
using RuleBasedFilterLibrary.Services;

namespace RuleBasedFilterLibrary.Middleware;

public class RuleBasedRequestFilterMiddleware(
    RequestDelegate next,
    IRulesLoaderService rulesLoaderService,
    string configurationFilename)
{
    private readonly RequestDelegate _next = next;
    private readonly List<RequestRule> _rules = rulesLoaderService.LoadRulesFromConfigurationFile(configurationFilename);

    public async Task InvokeAsync(HttpContext context)
    {
        var userIp = context.Connection.RemoteIpAddress?.ToString();

        var requestParameters = System.Web.HttpUtility.ParseQueryString(context.Request.QueryString.Value);
        var userRequest = new UserRequest
        {
            IpAddress = userIp ?? string.Empty,
            Parameters = requestParameters
        };

        if (context.Session.Keys.Contains("userIp"))
            Console.WriteLine($"Got request from {context.Session.GetString("userIp")} again");
        else
        {
            var userRequestsCollection = new  { userRequest };
            context.Session.SetString("userIp", userIp);
            Console.WriteLine($"Got request from {context.Session.GetString("userIp")} for the first time!");
        }

        var request = context.Request;

        foreach (var rule in _rules)
        {
            var validationResult = await rule.IsRequestValid(request);
            if (!validationResult)
            {
                context.Response.StatusCode = 403;
                return;
            }
        }
        
        await _next(context);
    }
}
