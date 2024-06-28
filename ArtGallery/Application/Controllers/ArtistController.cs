using ArtGallery.DTO;
using ArtGallery.Utils;
using FluentValidation;
using ArtGallery.Models;
using Microsoft.AspNetCore.Mvc;
using ArtGallery.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace ArtGallery.Application.Controllers
{
	[ApiController]
	[Route("api/artist")]
	public class ArtistController(IArtistService service, IValidator<ArtistDTO> validator) : ControllerBase
	{
		private readonly IArtistService _service = service;
		private readonly IValidator<ArtistDTO> _validator = validator;

		//
		//	Summary:
		//		Lists all the all artists from the database.
		//	Returns:
		//		200 Status Code with a paginated response that has a list of partial data.
		//		500 Status Code with the error message.
		[HttpGet]
		public async Task<ActionResult<List<Artist>>> Get()
		{
			try
			{
				var artists = await _service.All();
				return Ok(artists);
			}
			catch (System.Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		//
		//	Summary:
		//		Lists all the all artists that matches the query received.
		//	Returns:
		//		200 Status Code with a paginated response that has a list of partial data.
		//		500 Status Code with the error message.
		[HttpGet("/artist/q")]
		public async Task<ActionResult<List<PartialArtistDTO>>> GetQuery([FromQuery] ArtistQueryParams queryParams, [FromQuery] int page = 1)
		{
			var artists = await _service.PaginatedQuery(queryParams, page);
			return Ok(artists);
		}

		//
		//	Summary:
		//		Lists all the all artists from the database in a partial and paginated format.
		//	Returns:
		//		200 Status Code with a paginated response that has a list of partial entity.
		//		500 Status Code with the error message.
		[HttpGet("partial/paginate")]
		public async Task<ActionResult<PaginatedResponse<PartialArtistDTO>>> GetPartialPaginated([FromQuery] int pageIndex = 1)
		{
			try
			{
				var response = await _service.PartialPaginated(pageIndex);
				return Ok(response);
			}
			catch (System.Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		//
		//	Summary:
		//		Lists all the all artists from the database in a partial format.
		//	Returns:
		//		200 Status Code with a list of entity.
		//		500 Status Code with the error message.
		[HttpGet("partial")]
		public async Task<ActionResult<List<PartialArtistDTO>>> GetPartial()
		{
			try
			{
				var artists = await _service.Partial();
				return Ok(artists);
			}
			catch (System.Exception e)
			{
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
		public async Task<ActionResult<Artist>> GetBySlug(string slug)
		{
			try
			{
				var artist = await _service.FindBySlug(slug);
				return artist != null ? Ok(artist) : NotFound();
			}
			catch (System.Exception e)
			{
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
		public async Task<ActionResult<Artist>> GetById(int id)
		{
			try
			{
				var artist = await _service.FindById(id);
				return artist != null ? Ok(artist) : NotFound();
			}
			catch (System.Exception e)
			{
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
		public async Task<ActionResult<Artist>> Post([FromBody] ArtistDTO artist)
		{
			try
			{
				var validation = _validator.Validate(artist);
				if (!validation.IsValid) return BadRequest(ModelState);
				var create_artist = await _service.Save(artist);
				return Ok(create_artist);
			}
			catch (System.Exception e)
			{
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
		public async Task<ActionResult<bool>> Delete(int id)
		{
			try
			{
				var delete = await _service.Delete(id);
				return delete == null ? NotFound(false) : Ok(true);
			}
			catch (System.Exception e)
			{
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
		public async Task<ActionResult<Artist>> Patch(int id, [FromBody] UpdateArtistDTO artist)
		{
			try
			{
				var update = await _service.Update(id, artist);
				if (update == null) return NotFound();
				return Ok(update);
			}
			catch (System.Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}
	}
}