using Shared.Models;

namespace User_API.DTO
{
	public class UserDto
	{
		public string? NameEN { get; set; }
		public string? NameJP { get; set; }
		public int Age { get; set; }
		public List<int> PokemonIds { get; set; } = new();

	}
}
