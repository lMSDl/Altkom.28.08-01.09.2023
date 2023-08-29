namespace WebApp.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static void Use1Middleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<Use1Middleware>();
        }
    }
}
