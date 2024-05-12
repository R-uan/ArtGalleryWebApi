using ArtGallery.Interfaces;

namespace ArtGallery.Controllers {
	public class ArtworkController(IArtworkService service) {
		private readonly IArtworkService _service = service;
	}
}
