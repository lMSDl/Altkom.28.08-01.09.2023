using Microsoft.AspNetCore.Builder;
using WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddTransient.....
builder.Services.AddTransient<Use1Middleware>();
builder.Services.AddTransient<MapRunMiddleware>();


var app = builder.Build();

//app.Services.GetService....



//app.UseMiddleware<Use1Middleware>();
app.Use1Middleware();

app.Map("/Tom", mapApp =>
{
    mapApp.Use(async (context, next) =>
    {
        await Console.Out.WriteLineAsync("Begin MapUSE");
        await next(context);
        await Console.Out.WriteLineAsync("End MapUSE");

    });


    mapApp.UseMiddleware<MapRunMiddleware>();
});

app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), mapApp =>
{
    mapApp.Run(async context =>
    {
        await Console.Out.WriteLineAsync("Begin MapRUN");
        await context.Response.WriteAsync($"Hello {context.Request.Query["name"]}");
        await Console.Out.WriteLineAsync("End MapRUN");
    });
});


app.UseMiddleware<Use2Middleware>();

app.Run(async context =>
{
    await Console.Out.WriteLineAsync("Begin RUN");
    await context.Response.WriteAsync("Hello in main Run");
    await Console.Out.WriteLineAsync("End RUN");
});











//if (app.Environment.IsDevelopment())
//{
//    app.MapGet("/", () => "Hello World! D");
//}
//if (app.Environment.IsStaging())
//{
//    app.MapGet("/", () => "Hello World! S");
//}
//if (app.Environment.IsProduction())
//{
//    app.MapGet("/", () => "Hello World! P");
//}
//if (app.Environment.IsEnvironment("ala ma kota"))
//{
//    app.MapGet("/", () => "Hello World! ala");
//}

app.Run();
