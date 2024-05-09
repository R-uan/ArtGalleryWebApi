using ArtGallery.Models;
using ArtGallery.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
namespace ArtGallery.Controllers;

[ApiController]
[Route("artist")]
public class ArtistController(IArtistService service, IValidator<Artist> validator) : ControllerBase {
	private readonly IArtistService _artistService = service;
	private readonly IValidator<Artist> _validator = validator;

	[HttpGet]
	public async Task<ActionResult<List<Artist>>> All() {
		try {
			var artists = await _artistService.GetAll();
			return Ok(artists);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpPost]
	public async Task<ActionResult<Artist>> Post(Artist artist) {
		try {
			ValidationResult validation = _validator.Validate(artist);
			if (!validation.IsValid) return BadRequest(validation.Errors);
			var create_artist = await _artistService.PostOne(artist);
			return create_artist;
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/partial")]
	public async Task<ActionResult<List<Artist>>> AllPartial() {
		try {
			var artists = await _artistService.GetAllPartial();
			return Ok(artists);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/{slug}")]
	public async Task<ActionResult<Artist>> OneBySlug(string slug) {
		try {
			var artist = await _artistService.GetOneBySlug(slug);
			return artist != null ? Ok(artist) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/{id:int}")]
	public async Task<ActionResult<Artist>> OneById(int id) {
		try {
			var artist = await _artistService.GetOneById(id);
			return artist != null ? Ok(artist) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpDelete("/{id:int}")]
	public async Task<ActionResult<bool>> Delete(int id) {
		try {
			var delete = await _artistService.DeleteOne(id);
			return delete == null ? NotFound() : delete == true ? Ok() : StatusCode(500);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpPatch("/{id:int}")]
	public async Task<ActionResult<bool>> Patch(int id, Artist artist) {
		try {
			ValidationResult validation = _validator.Validate(artist);
			if (!validation.IsValid) return BadRequest(validation.Errors);
			var update = await _artistService.UpdateOne(id, artist);
			if (update == null) return NotFound();
			return update == true ? Ok() : StatusCode(500);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}
}
