using Microsoft.EntityFrameworkCore;
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

			foreach (var pokemon in pokemonsToAdd)
			{
				if (!user.Pokemons.Contains(pokemon))
					user.Pokemons.Add(pokemon);
			}

			context.Update(user);
			await context.SaveChangesAsync();
			return true;
		}

		public async Task<List<User>> FindAllWithPokemons()
		{
			return await context.User
				.Include(u => u.Pokemons)
				.ToListAsync();
		}
	}
}
