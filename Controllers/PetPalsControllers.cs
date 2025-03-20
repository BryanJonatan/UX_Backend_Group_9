using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using PetPals_BackEnd_Group_9.Validators;
using Serilog;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PetPalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SearchAdoptionListValidator _adoptionListValidator;
        private readonly GetServiceListQueryValidator _serviceListValidator;

        public PetPalsController(
            IMediator mediator,
            SearchAdoptionListValidator adoptionListValidator,
            GetServiceListQueryValidator serviceListValidator)
        {
            _mediator = mediator;
            _adoptionListValidator = adoptionListValidator;
            _serviceListValidator = serviceListValidator;
        }

        [HttpGet("adoption-list")]
        public async Task<IActionResult> GetAdoptionList([FromQuery] SearchAdoptionListQuery query)
        {
            var validationResult = await _adoptionListValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Validation Error",
                    status = 400,
                    errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("services-list")]
        public async Task<IActionResult> GetServiceList([FromQuery] GetServiceListQuery query)
        {
            Log.Information("Received GetServiceList request");

            var validationResult = await _serviceListValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                Log.Warning("Validation failed: {@Errors}", validationResult.Errors);
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Validation Error",
                    status = 400,
                    errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                });
            }

            try
            {
                var result = await _mediator.Send(query);
                Log.Information("Returning {Count} services", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching service list");
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred. Please try again later."
                });
            }
        }
    }
}
