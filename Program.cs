//using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("{first}/{second}/{third}", async context =>
{
    await context.Response.WriteAsync("Request was routed");

    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value} \n");
    }
});

app.MapGet("capital/{country}", Capital.Endpoint);
app.MapGet("size/{city}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));

app.Run();
