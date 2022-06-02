using Microsoft.Extensions.Options;

namespace Practice
{
    public class QueryStringMiddleware
    {
        private RequestDelegate? next;

        public QueryStringMiddleware() { }
        public QueryStringMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "GET" && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                await context.Response.WriteAsync("Middleware with QueryStringMiddleware \n");
            }
            if (next != null)
            {
                await next(context);
            }
        }

    }

    public class LocationMiddleware
    {
        private RequestDelegate next;
        private MessageOptions options;

        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> mesOpts) 
        {
            next = nextDelegate;
            options = mesOpts.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{options.CityName}, {options.CountryName}");
            }
            else
            {
                await next(context);
            }
        }
    }
}
