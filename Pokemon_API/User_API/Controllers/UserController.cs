using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_API.DTO;
using User_API.Models;
using User_API.Repository;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private IUserRepository repository;
		IMapper mapper;
		public UserController(IUserRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet("")]
		[ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllUser()
		{
			var allUser = await repository.FindAll();
			return Ok(allUser);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUserById(int id)
		{
			var user = await repository.FindById(id);

			if (user == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			return Ok(user);
		}

		[HttpPost("")]
		[ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
		{
			User user = mapper.Map<User>(userDto);

			await repository.Create(user);

			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
		{
			var userToUpdate = await repository.FindById(id);
			if (userToUpdate == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			mapper.Map(userDto, userToUpdate);
			await repository.Update(userToUpdate);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var userToDelete = await repository.FindById(id);
			if (userToDelete == null)
				return NotFound(new { message = $"User with ID {id} not found." });

			await repository.Delete(id);
			return NoContent();
		}
	}
}
