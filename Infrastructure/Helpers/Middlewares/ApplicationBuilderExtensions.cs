using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Helpers.Middlewares;

public static class ApplicationBuilderExtensions
{

    public static IApplicationBuilder UseUserSessionValidator(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<UserSessionValidatorMiddleware>();
        return builder;
    }
}
