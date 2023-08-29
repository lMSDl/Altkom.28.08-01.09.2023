using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus.Fakers
{
    public class UserFaker : EntityFaker<User>
    {
        public UserFaker()
        {
            RuleFor(x => x.UserName, x => x.Internet.UserName());
            RuleFor(x => x.Password, x => x.Internet.Password(length: 18));
            RuleFor(x => x.Roles, x => (Roles)x.Random.Int(1, (int)Math.Pow(2, Enum.GetValues<Roles>().Length) - 1));
        }
    }
}
