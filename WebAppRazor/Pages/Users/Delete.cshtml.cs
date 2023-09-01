using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace WebAppRazor.Pages.Users
{
    public class DeleteModel : UserPageModel
    {
		public DeleteModel(IUsersService service) : base(service)
		{
		}

        public User? SelectedUser { get; set; } 

		public async Task OnGet(int id)
        {
            SelectedUser = await Service.ReadAsync(id);
        }

        [BindProperty]
        public int Id { get; set; }
        public async Task<IActionResult> OnPost()
        {
            await Service.DeleteAsync(Id);
            return RedirectToPage("./Index");
        }
    }
}
