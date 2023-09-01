using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
	[AutoValidateAntiforgeryToken]
	[Authorize]
	public class UsersController : Controller
	{
		private IUsersService _service;

		public UsersController(IUsersService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			return View((await _service.ReadAsync()).OrderBy(x => x.Id));
		}

		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(string? phrase)
		{
			var items = await _service.ReadAsync();

			if (!string.IsNullOrWhiteSpace(phrase))
			{
				var properties = typeof(User).GetProperties().Where(x => x.CanRead).ToList();
				items = items.Where(item => properties.Any(property =>
					property.GetValue(item)?.ToString()?.Contains(phrase, StringComparison.InvariantCultureIgnoreCase) ?? false)).ToList();
			}

			return View(nameof(Index), items);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var user = await _service.ReadAsync(id);

			return View(user);
		}

		[HttpPost]
		[Authorize(Roles = nameof(Models.Roles.Delete))]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _service.DeleteAsync(id);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var user = await _service.ReadAsync(id);
			return View(user);
		}

		//public async Task<IActionResult> EditUser(int id, string userName, string password)
		public async Task<IActionResult> EditUser(int id, User editedUser)
		//public async Task<IActionResult> EditUser(int id, [Bind("UserName", "Password")]User editedUser)
		{
			/*var duplicate = await _service.GetUserByUserNameAsync(editedUser.UserName);
			if(duplicate != null && (id == 0 || id != duplicate.Id))
			{
				ModelState.AddModelError(nameof(Models.User.UserName), "User name must be unique");
			}
			*/
			if(!ModelState.IsValid)
			{
				return View(nameof(Edit));
			}

			editedUser.Password = BCrypt.Net.BCrypt.HashPassword(editedUser.Password);

			if (id == 0)
			{
				await _service.CreateAsync(editedUser);
			}
			else
			{
				var user = await _service.ReadAsync(id);

				user.UserName = editedUser.UserName;
				user.Password = editedUser.Password;

				await _service.UpdateAsync(id, user);
			}

			return RedirectToAction(nameof(Index));
		}

		[Authorize(Policy = "OnlyForPaul")]
		public IActionResult Add()
		{
			return View(nameof(Edit), new User());
		}
	}
}
