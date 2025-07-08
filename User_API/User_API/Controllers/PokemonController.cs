using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_API.Data;
using User_API.Models;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : ControllerBase
	{
		private readonly ApiContext _context;

		public PokemonController(ApiContext context)
		{
			_context = context;
		}

		[HttpGet("GetPokemons")]
		public async Task<IActionResult> GetPokemons()
		{
			var result = await _context.Pokemon.Select(x => new Pokemon
			{
				Id = x.Id,
				Name = x.Name,
				Type = x.Type,
			}).ToListAsync();

			return Ok(result);
		}

		[HttpPost("CreatePokemon")]
		public IActionResult CreatePokemon([FromBody] Pokemon pokemon)
		{
			_context.Pokemon.Add(pokemon);
			_context.SaveChanges();

			return Ok(pokemon);
		}

		[HttpPut("EditPokemon")]
		public async Task<IActionResult> EditPokemon([FromBody] Pokemon pokemon)
		{
			var rows = await _context.Pokemon.Where(p => p.Id == pokemon.Id).ExecuteUpdateAsync(x => 
				x.SetProperty(x => x.Name, pokemon.Name).SetProperty(x => x.Type, pokemon.Type)
			);

			return Ok(pokemon);
		}

		[HttpDelete("DeletePokemon")]
		public async Task<IActionResult> DeletePokemon(int id)
		{
			var rows = await _context.Pokemon.Where(p => p.Id == id).ExecuteDeleteAsync();

			return Ok(true);
		}
	}
}
