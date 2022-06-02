using Microsoft.Extensions.Options;
using Practice;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<MessageOptions>(options => { options.CityName = "Kyiv"; });

var app = builder.Build();

app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
{
    Practice.MessageOptions options = msgOpts.Value;
    await context.Response.WriteAsync($"{options.CountryName}, {options.CityName}, {msgOpts.Value.CountryName}");
});


app.MapGet("/", () => "Hello World!");

app.Run();
