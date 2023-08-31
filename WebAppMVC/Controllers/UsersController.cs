﻿using Microsoft.AspNetCore.Mvc;
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
		//public async Task<IActionResult> EditUser(User editedUser)
		public async Task<IActionResult> EditUser(int id, [Bind("UserName", "Password")]User editedUser)
		{
			var user = await _service.ReadAsync(id);

			user.UserName = editedUser.UserName;
			user.Password = editedUser.Password;

			await _service.UpdateAsync(id, user);

			return RedirectToAction(nameof(Index));
		}
	}
}
