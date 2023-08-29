using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICrudService<User>>(x => 
    new CrudService<User>(x.GetService<EntityFaker<User>>()!, x.GetService<IConfiguration>().GetValue<int>("Services:Bogus:Count")));
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

var app = builder.Build();


app.MapGet("/Users", (ICrudService<User> service) =>  service.ReadAsync());
app.MapGet("/Users/{id}", (int id, ICrudService<User> service) => service.ReadAsync(id));
app.MapPost("/Users", (User user, ICrudService<User> service) => service.CreateAsync(user));
app.MapPut("/Users/{id}", (int id, User user, ICrudService<User> service) => service.UpdateAsync(id, user));
app.MapDelete("/Users/{id}", (int id, ICrudService<User> service) => service.DeleteAsync(id));

app.MapGet("/", () => "Hello World!");

app.Run();
