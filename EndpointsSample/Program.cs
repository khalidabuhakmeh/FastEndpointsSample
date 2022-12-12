using EndpointsSample;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISystemClock, SystemClock>();
builder.Services.AddScoped<UserOverrideMiddleware>();
builder.Services.AddFastEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<UserOverrideMiddleware>();
}

app.UseFastEndpoints(c => {
    // everything is anonymous for this sample
    c.Endpoints.Configurator = epd => epd.AllowAnonymous();
});
app.Run();

public partial class Program {}

