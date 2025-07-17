using Shared.Repository;
using User_API.Data;
using User_API.Models;

namespace User_API.Repository
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(ApiContext context) : base(context){}
	}
}
