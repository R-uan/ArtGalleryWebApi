using ArtGallery.Models;
using ArtGallery.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ArtGallery.Interfaces;
using ArtGallery.DTO;
using ArtGallery.Utils;

namespace ArtGallery.Controllers {
	[ApiController]
	[Route("/museum")]
	public class MuseumController(IMuseumService service, IValidator<Museum> validator) : ControllerBase {
		private readonly IMuseumService _service = service;
		private readonly IValidator<Museum> _validator = validator;

		[HttpGet]
		public async Task<ActionResult<List<Museum>>> All() {
			try {
				var museum = await _service.GetAll();
				return Ok(museum);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("/museum/q")]
		public async Task<ActionResult<List<PartialMuseumDTO>>> QuerySearch([FromQuery] MuseumQueryParams queryParams, [FromQuery] int page = 1) {
			var artists = await _service.PaginatedQuery(queryParams, page);
			return Ok(artists);
		}

		[HttpPost]
		public async Task<ActionResult<Museum>> Post(MuseumDTO museum) {
			try {
				if (!ModelState.IsValid) return BadRequest(ModelState);
				var create_museum = await _service.PostOne(museum);
				return create_museum;
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("partial")]
		public async Task<ActionResult<List<PartialMuseumDTO>>> PartialMuseums() {
			try {
				var museum = await _service.GetAllPartial();
				return Ok(museum);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("partial/paginate")]
		public async Task<ActionResult<PaginatedResponse<PartialMuseumDTO>>> PaginatedPartial([FromQuery] int page_index = 1, int page_size = 20) {
			try {
				var response = await _service.GetAllPartialPaginated(page_index, page_size);
				return Ok(response);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("{slug}")]
		public async Task<ActionResult<Museum>> OneBySlug(string slug) {
			try {
				var museum = await _service.GetOneBySlug(slug);
				return museum != null ? Ok(museum) : NotFound();
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Museum>> OneById(int id) {
			try {
				var museum = await _service.GetOneById(id);
				return museum != null ? Ok(museum) : NotFound();
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
		public async Task<ActionResult<Museum?>> Patch(int id, UpdateMuseumDTO museum) {
			try {
				var update = await _service.UpdateOne(id, museum);
				if (update == null) return NotFound();
				return Ok(update);
			} catch (System.Exception e) {
				return StatusCode(500, e.Message);
			}
		}
	}
}