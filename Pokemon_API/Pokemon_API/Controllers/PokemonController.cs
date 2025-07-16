using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokemon_API.DTO;
using Pokemon_API.Repository;
using Shared.Models;
using Shared.Repository;
using System.Threading.Tasks;
using User_API.Data;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : ControllerBase
	{
		private readonly IPokemonRepository repository;

		public PokemonController(IPokemonRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllPokemons()
		{
			var result = await repository.FindAll();

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPokemonById(int id)
		{
			var result = await repository.FindById(id);

			return Ok(result);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto pokemonDto)
		{
			await repository.Create(new Pokemon { Name = pokemonDto.Name, Type = pokemonDto.Type});

			return Ok(true);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon pokemon)
		{
			repository.Update(pokemon);
			return Ok(pokemon);
		}

		[HttpDelete("id")]
		public async Task<IActionResult> DeletePokemon(int id)
		{
			await repository.Delete(id);

			return Ok(true);
		}
	}
}
