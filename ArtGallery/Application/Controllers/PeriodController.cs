using ArtGallery.Models;
using ArtGallery.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ArtGallery;

[ApiController]
[Route("period")]
public class PeriodController(IPeriodService service, IValidator<PeriodDTO> validator) : ControllerBase {
	private readonly IPeriodService _service = service;
	private readonly IValidator<PeriodDTO> _validator = validator;

	//
	//
	//
	//
	[HttpGet]
	public async Task<ActionResult<List<Period>>> Get() {
		try {
			var periods = await _service.GetPeriods();
			return Ok(periods);
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}

	//
	//
	//
	//
	[HttpGet("/partial")]
	public async Task<ActionResult<List<PartialPeriod>>> GetPartial() {
		try {
			var periods = await _service.GetPartialPeriods();
			return Ok(periods);
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}

	//
	//
	//
	//
	[HttpGet("{id:int}")]
	public async Task<ActionResult<Period?>> OneById(int id) {
		try {
			var period = await _service.GetOnePeriod(id);
			return period != null ? Ok(period) : NotFound($"Period was not found.");
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}

	//
	//
	//
	//
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<Period?>> Post([FromBody] PeriodDTO period) {
		try {
			var validation = _validator.Validate(period);
			if (validation.IsValid) {
				var save = await _service.PostPeriod(period);
				return Ok(save);
			} else return BadRequest(validation.Errors);
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}

	//
	//
	//
	//
	[Authorize]
	[HttpDelete("{id:int}")]
	public async Task<ActionResult<bool?>> Delete(int id) {
		try {
			var delete = await _service.DeletePeriod(id);
			return delete == true ? Ok(true) : NotFound("Period not found.");
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}
}
