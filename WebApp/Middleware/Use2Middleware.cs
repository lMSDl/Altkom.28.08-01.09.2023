namespace WebApp.Middleware
{
    public class Use2Middleware
    {

        private readonly RequestDelegate _next;

        public Use2Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await Console.Out.WriteLineAsync("Begin USE2");
            await _next(context);
            await Console.Out.WriteLineAsync("End USE2");
        }
    }
}
