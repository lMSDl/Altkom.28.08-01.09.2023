var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddTransient.....

var app = builder.Build();

//app.Services.GetService....


app.Use(async (context, next) =>
{
    await Console.Out.WriteLineAsync("Begin USE1");
    await next(context);
    await Console.Out.WriteLineAsync("End USE1");

});


app.Map("/Tom", mapApp =>
{
    mapApp.Use(async (context, next) =>
    {
        await Console.Out.WriteLineAsync("Begin MapUSE");
        await next(context);
        await Console.Out.WriteLineAsync("End MapUSE");

    });

    mapApp.Run(async context =>
    {
        await Console.Out.WriteLineAsync("Begin MapRUN");
        await context.Response.WriteAsync("Hello Tom");
        await Console.Out.WriteLineAsync("End MapRUN");
    });
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


app.Use(async (context, next) =>
{
    await Console.Out.WriteLineAsync("Begin USE2");
    await next(context);
    await Console.Out.WriteLineAsync("End USE2");

});

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
