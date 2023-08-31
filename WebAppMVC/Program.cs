using Microsoft.Extensions.FileProviders;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IUsersService>(x => new UsersService(x.GetService<EntityFaker<User>>()!, x.GetService<IConfiguration>().GetValue<int>("Services:Bogus:Count")));
//builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

builder.Services.AddScoped<IUsersService, DAL.DbFirst.Services.UsersService>();
builder.Services.AddTransient<ICrudService<User>>(x => x.GetService<IUsersService>()!);


builder.Services.AddSqlServer<DAL.DbFirst.ASPNETContext>(builder.Configuration.GetConnectionString("ASPNET"));


builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(x => x.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Program)));

builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(x =>
{
    x.SetDefaultCulture("en");
    x.AddSupportedCultures("en-us", "pl");
	x.AddSupportedUICultures("en-us", "pl");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

/*app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "public")),
    RequestPath = "/publiczny",
    OnPrepareResponse = x => x.Context.Response.Headers.Add("Cache-Control", "max-age=600000")
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "public")),
    RequestPath = "/publiczny"
});*/

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "public")),
    RequestPath = "/publiczny",
    EnableDirectoryBrowsing = true,
});

app.UseRequestLocalization();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


public partial class Program
{ } 