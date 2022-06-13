//using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts =>
{
    opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint));
});

var app = builder.Build();

app.MapGet("{numder:int}", async context =>
{
    await context.Response.WriteAsync($"Routed with int restriction with number --- " +
        $"{context.Request.RouteValues.Values.First()}");
}).Add(b => ((RouteEndpointBuilder)b).Order = 1);

app.MapGet("{number:double}", async context =>
{
    await context.Response.WriteAsync($"Routed with double restriction with number - " +
        $"{context.Request.RouteValues.Values.Last()}");
}).Add(b => ((RouteEndpointBuilder)b).Order = 2);

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
