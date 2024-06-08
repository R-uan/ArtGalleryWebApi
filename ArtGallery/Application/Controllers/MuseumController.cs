namespace ArtGallery.Controllers;
using ArtGallery.DTO;
using FluentValidation;
using ArtGallery.Utils;
using ArtGallery.Models;
using ArtGallery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("/museum")]
public class MuseumController(IMuseumService service, IValidator<MuseumDTO> validator) : ControllerBase {
	private readonly IMuseumService _service = service;
	private readonly IValidator<MuseumDTO> _validator = validator;

	//
	//	Summary:
	//		Lists all the all museums from the database.
	//	Returns:
	//		200 Status Code with a paginated response that has a list of partial data.
	//		500 Status Code with the error message.
	[HttpGet]
	public async Task<ActionResult<List<Museum>>> Get() {
		try {
			var museum = await _service.All();
			return Ok(museum);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}
	//
	//	Summary:
	//		Lists all the all museums that matches the query received.
	//	Returns:
	//		200 Status Code with a paginated response that has a list of partial data.
	//		500 Status Code with the error message.
	[HttpGet("/museum/q")]
	public async Task<ActionResult<List<PartialMuseumDTO>>> GetQuery([FromQuery] MuseumQueryParams queryParams, [FromQuery] int page = 1) {
		var artists = await _service.PaginatedQuery(queryParams, page);
		return Ok(artists);
	}

	//
	//	Summary:
	//		Lists all the all museums from the database in a partial format.
	//	Returns:
	//		200 Status Code with a list of entity.
	//		500 Status Code with the error message.
	[HttpGet("partial")]
	public async Task<ActionResult<List<PartialMuseumDTO>>> GetPartial() {
		try {
			var museum = await _service.Partial();
			return Ok(museum);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Lists all the all museums from the database in a partial and paginated format.
	//	Returns:
	//		200 Status Code with a paginated response that has a list of partial entity.
	//		500 Status Code with the error message.
	[HttpGet("partial/paginate")]
	public async Task<ActionResult<PaginatedResponse<PartialMuseumDTO>>> GetPartialPaginated([FromQuery] int pageIndex = 1) {
		try {
			var response = await _service.PartialPaginated(pageIndex);
			return Ok(response);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Finds the record associated with the given SLUG.
	//	Returns:
	//		200 Status Code with the found entity
	// 		404 Status Code
	// 		500 Status Code with the error message.
	[HttpGet("{slug}")]
	public async Task<ActionResult<Museum>> GetBySlug(string slug) {
		try {
			var museum = await _service.FindBySlug(slug);
			return museum != null ? Ok(museum) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Finds the record associated with the given ID.
	//	Returns:
	//		200 Status Code with the found entity
	// 		404 Status Code
	// 		500 Status Code with the error message.
	[HttpGet("{id:int}")]
	public async Task<ActionResult<Museum>> GetById(int id) {
		try {
			var museum = await _service.FindById(id);
			return museum != null ? Ok(museum) : NotFound();
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Creates a Artwork Record from the data received from the body.
	//	Returns:
	//		200 Status Code and the created entity.
	//		400 Status Code and the validation error message.
	//	 	500 Status Code and the error message.
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<Museum>> Post(MuseumDTO museum) {
		try {
			var validation = _validator.Validate(museum);
			if (!validation.IsValid) return BadRequest(ModelState);
			var create_museum = await _service.Save(museum);
			return create_museum;
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Deletes the record related to the ID provided.
	//	Returns:
	//		200 Status Code and a boolean of the value true.
	//		500 Status Code and a boolean of the value false.
	[Authorize]
	[HttpDelete("{id:int}")]
	public async Task<ActionResult<bool>> Delete(int id) {
		try {
			var delete = await _service.Delete(id);
			return delete == null ? NotFound(false) : Ok(true);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}

	//
	//	Summary:
	//		Update the record related to the ID with the data received from the body.
	//	Returns:
	//		200 Status Code and the updated entity.
	//		500 Status Code with error message.
	[Authorize]
	[HttpPatch("{id:int}")]
	public async Task<ActionResult<Museum?>> Patch(int id, UpdateMuseumDTO museum) {
		try {
			var update = await _service.Update(id, museum);
			if (update == null) return NotFound();
			return Ok(update);
		} catch (System.Exception e) {
			return StatusCode(500, e.Message);
		}
	}
}
