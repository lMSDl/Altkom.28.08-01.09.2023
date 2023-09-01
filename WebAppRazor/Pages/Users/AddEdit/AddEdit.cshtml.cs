using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users.AddEdit
{
    public class AddEditModel : UserPageModel
    {
		public AddEditModel(IUsersService service) : base(service)
		{
		}

		[BindProperty(SupportsGet = true)]
		public int Id { get; set; }

		[BindProperty]
		public User? SelectedUser { get; set; } 

		public async Task OnGet()
        {
			SelectedUser = await Service.ReadAsync(Id);
        }

		public async Task<IActionResult> OnPost()
		{
			if (SelectedUser.Id == 0)
			{
				await Service.CreateAsync(SelectedUser);
			}
			else
			{

				var user = await Service.ReadAsync(SelectedUser.Id);

				user.UserName = SelectedUser.UserName;
				user.Password = SelectedUser.Password;

				await Service.UpdateAsync(SelectedUser.Id, user);
			}

			return RedirectToPage("../Index");
		}
    }
}
