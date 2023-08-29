using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bogus
{
    public class UsersService : CrudService<User>, IUsersService
    {
        public UsersService(EntityFaker<User> faker, int count) : base(faker, count)
        {
        }

        public Task<User?> GetUserByUserNameAsync(string userName)
        {
            return Task.FromResult(Entities.SingleOrDefault(x => x.UserName == userName));
        }
    }
}
