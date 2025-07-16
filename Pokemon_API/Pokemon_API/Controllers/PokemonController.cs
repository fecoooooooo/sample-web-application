using AutoMapper;
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
		private readonly IMapper mapper;
		public PokemonController(IPokemonRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllPokemons()
		{
			var allPokemon = await repository.FindAll();
			allPokemon = allPokemon.OrderBy(x => x.Id).ToList();

			return Ok(allPokemon);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPokemonById(int id)
		{
			var pokemon = await repository.FindById(id);

			if (pokemon == null)
				return NotFound();

			return Ok(pokemon);
		}

		[HttpGet("list-by-type")]
		public async Task<IActionResult> GetPokemonsByType()
		{
			var grouped = await repository.GetGroupedByType();

			return Ok(grouped);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto dto)
		{
			Pokemon entity = mapper.Map<Pokemon>(dto);
			await repository.Create(entity);

			return CreatedAtAction(nameof(GetPokemonById), new { id = entity.Id }, entity);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePokemon(int id, [FromBody] PokemonDto dto)
		{
			var entityToUpdate = await repository.FindById(id);

			if (entityToUpdate == null)
				return NotFound(new { message = $"Pokemon with ID {id} not found." });

			mapper.Map(dto, entityToUpdate);

			await repository.Update(entityToUpdate);
			
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePokemon(int id)
		{
			var pokemonToDelete = await repository.FindById(id);
			if(pokemonToDelete == null)
				return NotFound(new { message = $"Pokemon with ID {id} not found." });

			await repository.Delete(id);

			return NoContent();
		}
	}
}
