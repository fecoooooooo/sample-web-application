using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Repository;
using User_API.Data;

namespace Pokemon_API.Repository
{
	public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
	{
		public PokemonRepository(ApiContext context) : base(context){}

		public async Task<Dictionary<string, List<string>>> GetGroupedByType()
		{
			var allEntity = await FindAll();
				
			var grouped = allEntity.GroupBy(p => p.Type).ToDictionary(
				g => g.Key,
				g => g.Select(p => p.Name).ToList()
			);

			return grouped;
		}
	}
}
