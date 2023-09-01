using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
	public class UserPageModel : PageModel
	{
		public IUsersService Service { get; }

		public UserPageModel(IUsersService service)
		{
			Service = service;
		}
	}
}
