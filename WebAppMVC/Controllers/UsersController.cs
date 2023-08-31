using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAppMVC.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class UsersController : Controller
	{
		private ICrudService<User> _service;

		public UsersController(ICrudService<User> service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.ReadAsync());
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
	}
}
