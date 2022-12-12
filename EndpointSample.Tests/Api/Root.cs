using System.Security.Claims;
using EndpointSample.Tests.Helpers;
using EndpointsSample;
using Microsoft.AspNetCore.Authentication;
using Xunit.Abstractions;
using static Microsoft.Extensions.DependencyInjection.ServiceDescriptor;

namespace EndpointSample.Tests.Api;

public class Root
{
    private readonly ITestOutputHelper _output;
    
    private App App { get; }
    private TestSystemClock Clock { get; } = new();
    private ClaimsPrincipal User { get; } = new();

    public Root(ITestOutputHelper output)
    {
        _output = output;
        App = new(
            UserOverride.With(User),
            Scoped<ISystemClock>(_ => Clock)
        );
    }

    [Fact]
    public async Task CanGetRootEndpoint()
    {
        // Arrange
        Clock.SetTime(0, 0);
        // Act
        var client = App.CreateClient();
        var result = await client.GetStringAsync("/");
        // Assert
        Assert.Equal("Hello, World @ 12:00:00 AM", result);
        
        _output.WriteLine(result);
    }

    [Fact]
    public async Task CanGetRootWithUserEndpoint()
    {
        // Arrange
        Clock.SetTime(0, 0);

        User.WithIdentity(
            new Claim(ClaimTypes.Name, "Khalid")
        );

        // Act
        var client = App.CreateClient();
        var result = await client.GetStringAsync("/");
        // Assert
        Assert.Equal("Hello, World @ 12:00:00 AM\nKhalid", result);
        
        _output.WriteLine(result);
    }
}