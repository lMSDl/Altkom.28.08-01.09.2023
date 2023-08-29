using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsersService>(x => new UsersService(x.GetService<EntityFaker<User>>()!, x.GetService<IConfiguration>().GetValue<int>("Services:Bogus:Count")));
builder.Services.AddTransient<ICrudService<User>>(x => x.GetService<IUsersService>()!);
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

var app = builder.Build();


app.MapGet("/Users", (ICrudService<User> service) =>  service.ReadAsync());
app.MapGet("/Users/{id}", (int id, ICrudService<User> service) => service.ReadAsync(id));
app.MapPost("/Users", (User user, ICrudService<User> service) => service.CreateAsync(user));
app.MapPut("/Users/{id}", (int id, User user, ICrudService<User> service) => service.UpdateAsync(id, user));
app.MapDelete("/Users/{id}", (int id, ICrudService<User> service) => service.DeleteAsync(id));

app.MapPost("/Login", async (HttpContext context, IUsersService service) => {

    var login = context.Request.Headers["login"];
    var password = context.Request.Headers["password"];

    var user = await service.GetUserByUserNameAsync(login);

    if(user?.Password == password)
    {
        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Expires = DateTime.Now.AddMinutes(5);


    }



});


app.MapGet("/", () => "Hello World!");

app.Run();
