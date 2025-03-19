using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Validators;
namespace PetPals_BackEnd_Group_9.Controllers
{

    [ApiController]
    [Route("api/v1")]
    public class PetPalsControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetPalsControllers(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("adoption-list")]
        public async Task<IActionResult> Get([FromQuery] SearchAdoptionListQuery query)
        {
            var validator = new SearchAdoptionListValidator();
            var validationResult = await validator.ValidateAsync(query);

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
    }
}
