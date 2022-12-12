using System.Security.Claims;

namespace EndpointSample.Tests.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static void WithIdentity(this ClaimsPrincipal user, params Claim[] claims)
        => user.AddIdentity(new ClaimsIdentity(claims, "test_auth"));
}