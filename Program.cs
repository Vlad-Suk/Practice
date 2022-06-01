var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


((IApplicationBuilder)app).Map("/branch", branch =>
{
    branch.UseMiddleware<Practice.QueryStringMiddleware>();

    branch.Run(async (context) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });

});

app.UseMiddleware<Practice.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
