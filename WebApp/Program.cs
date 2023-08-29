var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddTransient.....

var app = builder.Build();

//app.Services.GetService....

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
