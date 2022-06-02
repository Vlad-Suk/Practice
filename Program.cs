var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


((IApplicationBuilder)app).Map("/branch", branch =>
{
    //branch.UseMiddleware<Practice.QueryStringMiddleware>();

    branch.Run(new Practice.QueryStringMiddleware().Invoke);

});

app.UseMiddleware<Practice.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
