//using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("{first:int}/{second:bool}", async context =>
{
    await context.Response.WriteAsync("Request was routed");

    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value} \n");
    }
});

app.MapGet("capital/{country=france}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));

app.MapFallback(async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();
