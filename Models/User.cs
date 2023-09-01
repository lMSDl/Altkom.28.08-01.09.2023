using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : Entity
    {
		[DisplayName("Login")]
        [Required]
		public string UserName { get; set; } = "";
        [MinLength(8)]
        //[RegularExpression("")]
		public string Password { get; set; } = "";

        //[EmailAddress]
        //public string Email { get; set; }
        //[Phone]
        //public string Phone { get; set; }

        public Roles Roles { get; set; }
    }
}