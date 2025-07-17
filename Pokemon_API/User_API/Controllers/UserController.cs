using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Repository;
using Shared.Models;
using User_API.DTO;
using User_API.Models;
using User_API.Repository;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUserRepository userRepository;
		private IPokemonRepository pokemonRepository;
		IMapper mapper;
		public UserController(IUserRepository userRepository, IPokemonRepository pokeomonRepository, IMapper mapper)
		{
			this.userRepository = userRepository;
			this.pokemonRepository = pokeomonRepository;
			this.mapper = mapper;
		}

		[HttpGet("")]
		[ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllUser()
		{
			var allUser = await userRepository.FindAllWithPokemons();
			return Ok(allUser);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await userRepository.FindById(id);

			if (user == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			return Ok(user);
		}

		[HttpPost("")]
		[ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
		{
			User user = mapper.Map<User>(userDto);

			await userRepository.Create(user);

			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}


		[HttpPost("{id}/pokemons")]
		[ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateUserPokemons(int id, [FromBody] List<int> pokemonIdsToAdd)
		{
			var user = await userRepository.FindById(id);

			if(user == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			var pokemonsToAdd = new List<Pokemon>();
			foreach (int pid in pokemonIdsToAdd)
			{
				var pokemon = await pokemonRepository.FindById(pid);
				if(pokemon != null)
					pokemonsToAdd.Add(pokemon);
			}

			await userRepository.AddPokemonsToUser(user, pokemonsToAdd);

			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
		{
			var userToUpdate = await userRepository.FindById(id);
			if (userToUpdate == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			mapper.Map(userDto, userToUpdate);
			await userRepository.Update(userToUpdate);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var userToDelete = await userRepository.FindById(id);
			if (userToDelete == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			await userRepository.Delete(id);
			return NoContent();
		}
	}
}
