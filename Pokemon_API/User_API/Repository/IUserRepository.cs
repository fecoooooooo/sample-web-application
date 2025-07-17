using Shared.Models;
using Shared.Repository;
using User_API.Models;

namespace User_API.Repository
{
	public interface IUserRepository : IRepositoryBase<User>
	{
		Task<bool> AddPokemonsToUser(User user, List<Pokemon> pokemonsToAdd);
	}
}
