namespace WebApp.Middleware
{
    public class Use1Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await Console.Out.WriteLineAsync("Begin USE1");
            await next(context);
            await Console.Out.WriteLineAsync("End USE1");
        }
    }
}
