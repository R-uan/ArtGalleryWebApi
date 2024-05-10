using ArtGallery.Models;
using ArtGallery.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers;

[ApiController]
[Route("/museum")]
public class MuseumController : ControllerBase {
	private readonly IService<Museum, UpdateMuseum, MuseumPartial> _service;
	private readonly IValidator<Museum> _validator;

	public MuseumController(IService<Museum, UpdateMuseum, MuseumPartial> service, IValidator<Museum> validator) {
		_service = service;
		_validator = validator;
	}

	[HttpGet]
	public async Task<ActionResult<List<Museum>>> All() {
		try {
			var museum = await _service.GetAll();
			return Ok(museum);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpPost]
	public async Task<ActionResult<Museum>> Post(Museum museum) {
		try {
			ValidationResult validation = _validator.Validate(museum);
			if (!validation.IsValid) return BadRequest(validation.Errors);
			var create_museum = await _service.PostOne(museum);
			return create_museum;
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/partial")]
	public async Task<ActionResult<List<Museum>>> AllPartial() {
		try {
			var museum = await _service.GetAllPartial();
			return Ok(museum);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/{slug}")]
	public async Task<ActionResult<Museum>> OneBySlug(string slug) {
		try {
			var museum = await _service.GetOneBySlug(slug);
			return museum != null ? Ok(museum) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpGet("/{id:int}")]
	public async Task<ActionResult<Museum>> OneById(int id) {
		try {
			var museum = await _service.GetOneById(id);
			return museum != null ? Ok(museum) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpDelete("/{id:int}")]
	public async Task<ActionResult<bool>> Delete(int id) {
		try {
			var delete = await _service.DeleteOne(id);
			return delete == null ? NotFound() : delete == true ? Ok() : StatusCode(500);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	[HttpPatch("/{id:int}")]
	public async Task<ActionResult<bool>> Patch(int id, UpdateMuseum museum) {
		try {
			var update = await _service.UpdateOne(id, museum);
			if (update == null) return NotFound();
			return update == true ? Ok() : StatusCode(500);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}
}