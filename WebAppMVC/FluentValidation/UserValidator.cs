using FluentValidation;
using Services.Interfaces;

namespace WebAppMVC.FluentValidation
{
	public class UserValidator : AbstractValidator<Models.User>
	{
		public UserValidator(IUsersService service)
		{
			RuleFor(x => x.UserName)/*.NotNull()*/.NotEmpty()
				.Must((x, userName, _) =>
			{
				var duplicate = service.GetUserByUserNameAsync(userName).Result;
				return !(duplicate != null && (x.Id == 0 || x.Id != duplicate.Id));
			}).WithMessage("User name musi być unikalny");

			RuleFor(x => x.Password).MinimumLength(8).WithName("Hasło");
		}
	}
}
