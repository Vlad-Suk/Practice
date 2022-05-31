var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


((IApplicationBuilder)app).Map("/branch", branch =>
{
    branch.UseMiddleware<Practice.QueryStringMiddleware>();

    branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });

});

app.UseMiddleware<Practice.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
