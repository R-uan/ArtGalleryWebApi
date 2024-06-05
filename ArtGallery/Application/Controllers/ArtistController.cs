using FluentValidation;
using ArtGallery.Models;
using ArtGallery.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Controllers {
	[ApiController]
	[Route("/artist")]
	public class ArtistController(IArtistService service, IValidator<Artist> validator) : ControllerBase {
		private readonly IValidator<Artist> _validator = validator;
		private readonly IArtistService _service = service;

		[HttpGet]
		public async Task<ActionResult<List<Artist>>> All() {
			try {
				var artists = await _service.GetAll();
				return Ok(artists);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("/artist/q")]
		public async Task<ActionResult<List<PartialArtistDTO>>> QuerySearch([FromQuery] ArtistQueryParams queryParams, [FromQuery] int page = 1) {
			var artists = await _service.PaginatedQuery(queryParams, page);
			return Ok(artists);
		}

		[HttpGet("partial/paginate")]
		public async Task<ActionResult<PaginatedResponse<PartialArtistDTO>>> PaginatedPartial([FromQuery] int pageIndex = 1) {
			try {
				var response = await _service.GetAllPartialPaginated(pageIndex);
				return Ok(response);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult<Artist>> Post([FromBody] ArtistDTO artist) {
			try {
				if (!ModelState.IsValid) return BadRequest(ModelState);
				var create_artist = await _service.PostOne(artist);
				return Ok(create_artist);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("partial")]
		public async Task<ActionResult<List<PartialArtistDTO>>> PartialArtists() {
			try {
				var artists = await _service.GetAllPartial();
				return Ok(artists);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("{slug}")]
		public async Task<ActionResult<Artist>> OneBySlug(string slug) {
			try {
				var artist = await _service.GetOneBySlug(slug);
				return artist != null ? Ok(artist) : NotFound();
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}


		[HttpGet("{id:int}")]
		public async Task<ActionResult<Artist>> OneById(int id) {
			try {
				var artist = await _service.GetOneById(id);
				return artist != null ? Ok(artist) : NotFound();
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
		public async Task<ActionResult<Artist>> Patch(int id, [FromBody] UpdateArtistDTO artist) {
			try {
				var update = await _service.UpdateOne(id, artist);
				if (update == null) return NotFound();
				return Ok(update);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}
	}
}