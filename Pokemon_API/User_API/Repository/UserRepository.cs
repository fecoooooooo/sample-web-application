using Shared.Models;
using Shared.Repository;
using User_API.Data;
using User_API.Models;

namespace User_API.Repository
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(ApiContext context) : base(context){}

		public async Task<bool> AddPokemonsToUser(User user, List<Pokemon> pokemonsToAdd)
		{
			if(user.Pokemons == null)
				user.Pokemons = new List<Pokemon>();

			foreach(var pokemon in pokemonsToAdd)
			{
				user.Pokemons.Add(pokemon);
			}

			await context.SaveChangesAsync();
			return true;
		}
	}
}
