using System.Security.Claims;

namespace EndpointsSample;

public class UserOverrideMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userOverride = context.RequestServices.GetService<UserOverride>();
        if (userOverride is { Value : {} })
        {
            context.User = userOverride.Value;
        }

        await next(context);
    }
}

public record UserOverride(ClaimsPrincipal Value)
{
    public static ServiceDescriptor With(ClaimsPrincipal user)
        => ServiceDescriptor.Scoped<UserOverride>(_ => new(user));
};