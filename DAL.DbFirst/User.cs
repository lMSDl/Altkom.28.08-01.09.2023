using System;
using System.Collections.Generic;

namespace DAL.DbFirst
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
		public int Roles { get; set; }
	}
}
