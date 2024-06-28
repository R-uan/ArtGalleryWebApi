using System.Text;
using FluentValidation;
using ArtGallery.Models;
using Microsoft.AspNetCore.Mvc;
using ArtGallery.Interfaces.Services;

namespace ArtGallery.Application.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AdminController(IAdminService service, IValidator<Admin> validator) : ControllerBase
    {
        private readonly IValidator<Admin> _validator = validator;
        private readonly IAdminService _service = service;
        [HttpGet]
        public async Task<ActionResult<string>> Authenticate()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                var authorization = Request.Headers.Authorization.ToString();
                var auth_token = authorization.Substring("Basic ".Length).Trim();
                var credentialBytes = Convert.FromBase64String(auth_token);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                var token = await _service.Authenticate(username, password);
                return token == null ? Unauthorized() : Ok(token);
            }
            else return BadRequest("No credentials provided");
        }

        [HttpPost]
        public async Task<ActionResult<int?>> Register([FromBody] Admin admin)
        {
            try
            {
                var validate = _validator.Validate(admin);
                if (!validate.IsValid) return BadRequest(validate.Errors);
                return await _service.Register(admin.Username, admin.Password);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}