using ArtGallery.Models;
using ArtGallery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using ArtGallery.Utils;


namespace ArtGallery.Controllers {
	[ApiController]
	[Route("/artwork")]
	public class ArtworkController(IArtworkService service, IValidator<Artwork> validator) : ControllerBase {
		private readonly IArtworkService _service = service;
		private readonly IValidator<Artwork> _validator = validator;

		[HttpGet]
		public async Task<ActionResult<List<Artwork>>> All() {
			try {
				var artwork = await _service.GetAll();
				return Ok(artwork);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<Artwork>> Post(Artwork artwork) {
			try {
				ValidationResult validation = _validator.Validate(artwork);
				if (!validation.IsValid) return BadRequest(validation.Errors);
				var create_artist = await _service.PostOne(artwork);
				return create_artist;
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("partial")]
		public async Task<ActionResult<List<PartialArtwork>>> PartialArtworks() {
			try {
				var artwork = await _service.GetAllPartial();
				return Ok(artwork);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("partial/paginate")]
		public async Task<ActionResult<PaginatedResponse<PartialArtwork>>> PaginatedPartial([FromQuery] int page_index = 1, int page_size = 20) {
			try {
				var response = await _service.GetAllPartialPaginated(page_index, page_size);
				return Ok(response);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("{slug}")]
		public async Task<ActionResult<Artwork>> OneBySlug(string slug) {
			try {
				var artwork = await _service.GetOneBySlug(slug);
				return artwork != null ? Ok(artwork) : NotFound();
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Artwork>> OneById(int id) {
			try {
				var artwork = await _service.GetOneById(id);
				return artwork != null ? Ok(artwork) : NotFound();
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult<bool>> Delete(int id) {
			try {
				var delete = await _service.DeleteOne(id);
				return delete == null ? NotFound(false) : Ok(true);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpPatch("{id:int}")]
		public async Task<ActionResult<Artwork>> Patch(int id, UpdateArtwork artwork) {
			try {
				var update = await _service.UpdateOne(id, artwork);
				if (update == null) return NotFound();
				return Ok(update);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}
	}
}
