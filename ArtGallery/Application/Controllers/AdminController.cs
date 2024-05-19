using Microsoft.AspNetCore.Mvc;
namespace ArtGallery.Controllers {
	[ApiController]
	[Route("auth")]
	public class AdminController : ControllerBase {
		[HttpGet]
		public ActionResult<string> Authenticate() {
			return Ok("");
		}
	}
}