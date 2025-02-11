using Microsoft.AspNetCore.Http;

namespace RuleBasedFilterLibrary.Middleware.Services.RequestValidation;

public interface IRequestValidationService
{
    public Task<bool> IsValidAsync(HttpContext context);
}
