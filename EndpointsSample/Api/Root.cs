using System.Security.Claims;
using System.Text;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;

namespace EndpointsSample.Api;

public class Root : EndpointWithoutRequest<string>
{
    public ISystemClock Clock { get; set; }
        = default!;

    public override void Configure()
    {
        Get("/");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var message = $"Hello, World @ {Clock.UtcNow:hh:mm:ss tt}";
        
        if (User.Identity?.IsAuthenticated == true)
        {
            message += $"\n{User.Identity.Name}";
        }
        
        await SendStringAsync(
            message,
            cancellation: ct
        );
    }
}