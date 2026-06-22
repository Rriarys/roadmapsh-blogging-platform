using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.API.ApiExtensions;

public static class ValidationFilterExtensions
{
    /// <summary>
    /// Adds an endpoint filter that validates DTOs using data annotation attributes and returns a validation problem
    /// response on failure
    /// </summary>
    /// <typeparam name="T">The DTO type to validate</typeparam>
    /// <param name="builder">The route handler builder</param>
    /// <returns>The route handler builder to enable method chaining</returns>
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter(async (context, next) =>
        {
            // Look for the first argument of type T (the DTO we want to validate)
            var argument = context.Arguments.FirstOrDefault(x => x is T);

            if (argument is T dto)
            {
                var validationContext = new ValidationContext(dto);
                var validationResults = new List<ValidationResult>();

                // Built-in .NET validation checks all [Required] attributes
                if (!Validator.TryValidateObject(dto, validationContext, validationResults, true))
                {
                    // If there are errors, group them by property name (as in controllers)
                    var errors = validationResults
                        .GroupBy(x => x.MemberNames.FirstOrDefault() ?? string.Empty)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => x.ErrorMessage ?? "Invalid value").ToArray()
                        );

                    // Return standard 400 Bad Request with error details
                    return Results.ValidationProblem(errors);
                }
            }

            // If everything is valid, pass control to the next middleware (the endpoint itself)
            return await next(context);
        });
    }
}
