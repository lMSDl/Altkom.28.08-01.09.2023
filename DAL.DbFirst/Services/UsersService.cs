using AutoMapper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbFirst.Services
{
	public class UsersService : CrudService<Models.User, DAL.DbFirst.User>, IUsersService
	{
		public UsersService(ASPNETContext dbContext) : base(dbContext)
		{
		}

		public Task<Models.User?> GetUserByUserNameAsync(string userName)
		{
			throw new NotImplementedException();
		}
	}
}
