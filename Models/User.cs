namespace Models
{
    public class User : Entity
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public Roles Roles { get; set; }
    }
}