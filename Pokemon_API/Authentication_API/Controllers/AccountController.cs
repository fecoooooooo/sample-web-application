using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly JwtTokenHandler jwtTokenHandler;

		public AccountController(JwtTokenHandler jwtTokenHandler)
		{
			this.jwtTokenHandler = jwtTokenHandler;
		}

		[HttpPost("")]
		public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
		{
			var authenticationResponse = jwtTokenHandler.GenerateJwtToken(authenticationRequest);
			if (authenticationRequest == null)
				return Unauthorized();

			return authenticationResponse;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(int id)
		{

			return Ok();
		}
	}
}
