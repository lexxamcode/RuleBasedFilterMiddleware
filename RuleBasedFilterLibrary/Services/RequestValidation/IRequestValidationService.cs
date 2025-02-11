using Microsoft.AspNetCore.Http;

namespace RuleBasedFilterLibrary.Services.RequestValidation;

public interface IRequestValidationService
{
    public Task<bool> IsValidAsync(HttpContext context);
}
