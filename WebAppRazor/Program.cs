using Models;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUsersService, DAL.DbFirst.Services.UsersService>();
builder.Services.AddTransient<ICrudService<User>>(x => x.GetService<IUsersService>()!);

builder.Services.AddSqlServer<DAL.DbFirst.ASPNETContext>(builder.Configuration.GetConnectionString("ASPNET"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
