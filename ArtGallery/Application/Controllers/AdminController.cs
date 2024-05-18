using ArtGallery.Interfaces;
using ArtGallery.Models;
using Microsoft.AspNetCore.Mvc;
namespace ArtGallery.Controllers {
	[ApiController]
	[Route("auth")]
	public class AdminController(IAdminService service) : ControllerBase {
		private readonly IAdminService _service = service;

		[HttpGet]
		public async Task<ActionResult<string>> Authenticate([FromBody] Admin credentials) {
			if(credentials.Username != null && credentials.Password != null) {
				string? token = await _service.Authenticate(credentials.Username, credentials.Password);
				if(token == null) return Unauthorized();
				return Ok(token);
			}
			return BadRequest();
		}
	}
}