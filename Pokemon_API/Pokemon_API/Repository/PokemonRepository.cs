using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Repository;
using User_API.Data;

namespace Pokemon_API.Repository
{
	public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
	{

		public PokemonRepository(ApiContext context) : base(context)
		{
		}
	}
}
