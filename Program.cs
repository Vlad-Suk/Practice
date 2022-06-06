//using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("routing", async context =>
    {
        await context.Response.WriteAsync("Request was routed");
    });
    endpoints.MapGet("capital/uk", new Capital().Invoke);
    endpoints.MapGet("population/paris", new Population().Invoke);
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Terminal middleware reached");
});

app.Run();
