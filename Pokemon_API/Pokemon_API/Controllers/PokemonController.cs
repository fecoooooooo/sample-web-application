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
			var result = await repository.FindAll();
			result = result.OrderBy(x => x.Id).ToList();

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPokemonById(int id)
		{
			var result = await repository.FindById(id);

			return Ok(result);
		}

		[HttpPost("")]
		public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto dto)
		{
			Pokemon entity = mapper.Map<Pokemon>(dto);
			await repository.Create(entity);

			return Ok(true);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePokemon(int id, [FromBody] PokemonDto dto)
		{
			var entityToUpdate = await repository.FindById(id);
			
			if (entityToUpdate != null)
			{
				mapper.Map(dto, entityToUpdate);

				await repository.Update(entityToUpdate);
			}

			return Ok(true);
		}

		[HttpDelete("id")]
		public async Task<IActionResult> DeletePokemon(int id)
		{
			await repository.Delete(id);

			return Ok(true);
		}
	}
}
