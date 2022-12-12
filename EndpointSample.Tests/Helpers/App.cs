using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace EndpointSample.Tests.Helpers;

public class App : WebApplicationFactory<Program>
{
    private readonly ServiceDescriptor[] _overrides;

    public App(params ServiceDescriptor[]? overrides)
    {
        _overrides = overrides ?? Array.Empty<ServiceDescriptor>();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            foreach (var service in _overrides)
            {
                services.Replace(service);
            }
        });

        return base.CreateHost(builder);
    }
}