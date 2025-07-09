using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_API.Data;
using User_API.Models;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TempUserController : ControllerBase
	{
		private readonly ApiContext _context;

		public TempUserController(ApiContext context)
		{
			_context = context;
		}

		[HttpGet("GetTempUsers")]
		public async Task<IActionResult> GetTempUsers()
		{
			var result = await _context.TempUsers.Select(x => new TempUser
			{
				Id = x.Id,
				NameEN = x.NameEN,
				NameJP = x.NameJP,
				Age = x.Age

			}).ToListAsync();

			return Ok(result);
		}

	}
}
