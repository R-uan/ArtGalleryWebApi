using ArtGallery.Models;
using FluentValidation;
using ArtGallery.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Application.Controllers
{
    [ApiController]
    [Route("period")]
    public class PeriodController(IPeriodService service, IValidator<PeriodDTO> validator) : ControllerBase
    {
        private readonly IPeriodService _service = service;
        private readonly IValidator<PeriodDTO> _validator = validator;

        //
        //
        //
        //
        [HttpGet]
        public async Task<ActionResult<List<Period>>> Get()
        {
            try
            {
                var periods = await _service.All();
                return Ok(periods);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
                throw;
            }
        }
        //
        //
        //
        //
        [HttpGet("/partial")]
        public async Task<ActionResult<List<PartialPeriod>>> GetPartial()
        {
            try
            {
                var periods = await _service.Partial();
                return Ok(periods);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
                throw;
            }
        }
        //
        //
        //
        //
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Period?>> OneById(int id)
        {
            try
            {
                var period = await _service.FindById(id);
                return period != null ? Ok(period) : NotFound($"Period was not found.");
            }
            catch (System.Exception ex)
            {
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
        public async Task<ActionResult<Period?>> Post([FromBody] PeriodDTO period)
        {
            try
            {
                var validation = _validator.Validate(period);
                if (validation.IsValid)
                {
                    var save = await _service.Save(period);
                    return Ok(save);
                }
                else return BadRequest(validation.Errors);
            }
            catch (System.Exception ex)
            {
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
        public async Task<ActionResult<bool?>> Delete(int id)
        {
            try
            {
                var delete = await _service.Delete(id);
                return delete == true ? Ok(true) : NotFound("Period not found.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
                throw;
            }
        }
    }
}