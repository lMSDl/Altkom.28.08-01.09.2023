using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var signingKey = Encoding.Default.GetBytes(Guid.NewGuid().ToString());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsersService>(x => new UsersService(x.GetService<EntityFaker<User>>()!, x.GetService<IConfiguration>().GetValue<int>("Services:Bogus:Count")));
builder.Services.AddTransient<ICrudService<User>>(x => x.GetService<IUsersService>()!);
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        RequireExpirationTime = true
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/Users", (ICrudService<User> service) =>  service.ReadAsync());
app.MapGet("/Users/{id}", (int id, ICrudService<User> service) => service.ReadAsync(id));
app.MapPost("/Users", (User user, ICrudService<User> service) => service.CreateAsync(user));
app.MapPut("/Users/{id}", [Authorize(Roles = nameof(Roles.Update) + "," + nameof(Roles.Read))] (int id, User user, ICrudService<User> service) => service.UpdateAsync(id, user));
app.MapDelete("/Users/{id}", [Authorize(Roles = nameof(Roles.Delete))][Authorize(Roles = nameof(Roles.Delete))] (int id, ICrudService<User> service) => service.DeleteAsync(id));

app.MapPost("/Login", async (HttpContext context, IUsersService service) => {

    var login = context.Request.Headers["login"];
    var password = context.Request.Headers["password"];

    var user = await service.GetUserByUserNameAsync(login);

    if(user?.Password == password)
    {
        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Expires = DateTime.Now.AddMinutes(5);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        claims.AddRange(
        Enum.GetValues<Roles>().Where(x => user.Roles.HasFlag(x))
                               .Select(x => new Claim(ClaimTypes.Role, x.ToString()))
                               .ToList());
        tokenDescriptor.Subject = new ClaimsIdentity(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        await context.Response.WriteAsync(tokenHandler.WriteToken(token));
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }


});


app.MapGet("/", () => "Hello World!");

app.Run();
