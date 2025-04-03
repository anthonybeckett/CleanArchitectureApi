using CleanArchitectureApi.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitectureApi.Api.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errors = context
            .ModelState
            .Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        throw errors.Any(x => x.Contains("request"))
            ? new PayloadFormatException(errors)
            : new RequestValidationException(errors);
    }
}