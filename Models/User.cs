using System.ComponentModel;

namespace Models
{
    public class User : Entity
    {
		[DisplayName("Login")]
		public string UserName { get; set; } = "";

		public string Password { get; set; } = "";

        public Roles Roles { get; set; }
    }
}