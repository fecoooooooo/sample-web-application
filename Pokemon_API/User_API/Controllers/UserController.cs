using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TempUserController : ControllerBase
	{
		public TempUserController()
		{
		}

		[HttpGet("GetTempUsers")]
		public async Task<IActionResult> GetTempUsers()
		{

			return Ok();
		}

	}
}
