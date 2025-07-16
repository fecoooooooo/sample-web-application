using Shared.Models;
using Shared.Repository;

namespace Pokemon_API.Repository
{
	public interface IPokemonRepository : IRepositoryBase<Pokemon>
	{
		Task<Dictionary<string, List<string>>> GetGroupedByType();
	}
}
