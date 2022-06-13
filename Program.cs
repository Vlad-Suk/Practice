//using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts =>
{
    opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint));
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    Endpoint? end = context.GetEndpoint();
    if (end != null)
    {
        await context.Response.WriteAsync($"Name endpoint is {end.DisplayName} \n");
    }
    else
    {
        await context.Response.WriteAsync("Endpoint wasn't routed\n");
    }
    await next();
});

app.MapGet("{numder:int}", async context =>
{
    await context.Response.WriteAsync($"Routed with int restriction with number --- " +
        $"{context.Request.RouteValues.Values.First()}");
}).WithDisplayName("Int Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 1);

app.MapGet("{number:double}", async context =>
{
    await context.Response.WriteAsync($"Routed with double restriction with number - " +
        $"{context.Request.RouteValues.Values.Last()}");
}).WithDisplayName("Double Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 2);

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
