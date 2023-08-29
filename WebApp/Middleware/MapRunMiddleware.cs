namespace WebApp.Middleware
{
    public class MapRunMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await Console.Out.WriteLineAsync("Begin MapRUN");
            await context.Response.WriteAsync("Hello Tom");
            await Console.Out.WriteLineAsync("End MapRUN");
        }
    }
}
