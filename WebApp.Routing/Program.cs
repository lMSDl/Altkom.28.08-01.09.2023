var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseRouting();

app.Use(async (context, next) =>
{
    await next(context);
});

app.UseRouting();

app.Use(async (context, next) =>
{
    await next(context);
});

/*app.Map("/demo", mapApp =>
{

    mapApp.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("{name}", (string name) => $"Hello {name}");
    });

    mapApp.Run(async context =>
    {
        await context.Response.WriteAsync("Under construction!");
    });
});*/

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("Hello", async x => await  x.Response.WriteAsync("Hello world!") );
});*/
app.MapGet("/demo/{name}", (string name) => $"Hello {name}!");
app.MapGet("/Hello", () => "Hello World!");
app.MapGet("/", () => "Under construction");


/*app.Run(async context =>
{
    await context.Response.WriteAsync("Under construction!");
});*/

app.Run();
