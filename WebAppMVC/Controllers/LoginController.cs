using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace WebAppMVC.Controllers
{
	public class LoginController : Controller
	{
		private IUsersService _service;

		public LoginController(IUsersService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Login(Models.User user, string ReturnUrl)
		{
			var dbUser = await _service.GetUserByUserNameAsync(user.UserName);

			if (BCrypt.Net.BCrypt.Verify(user.Password, dbUser?.Password))
			{
				var claims = new List<Claim>
								{
									new Claim(ClaimTypes.Name, dbUser.UserName),
									new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
								};

				claims.AddRange(
				Enum.GetValues<Roles>().Where(x => dbUser.Roles.HasFlag(x))
									   .Select(x => new Claim(ClaimTypes.Role, x.ToString()))
									   .ToList());

				var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			}

			return Redirect(ReturnUrl);

		}
	}
}
