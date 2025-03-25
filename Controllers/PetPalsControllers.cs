using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using PetPals_BackEnd_Group_9.Validators;
using Serilog;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Handlers;

namespace PetPals_BackEnd_Group_9.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PetPalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SearchAdoptionListValidator _adoptionListValidator;
        private readonly GetServiceListQueryValidator _serviceListValidator;
        private readonly ILogger<PetPalsController> _logger;
        private readonly AdoptionTransactionValidator _validator;
        private readonly ServiceTransactionValidator _serviceTransactionValidator;

        public PetPalsController(
            IMediator mediator,
            SearchAdoptionListValidator adoptionListValidator,
            GetServiceListQueryValidator serviceListValidator, ILogger<PetPalsController> logger)
        {
            _mediator = mediator;
            _adoptionListValidator = adoptionListValidator;
            _serviceListValidator = serviceListValidator;
            _logger = logger;
            _validator = new AdoptionTransactionValidator();
            _serviceTransactionValidator = new ServiceTransactionValidator();
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

        [HttpGet("adoption-list/{slug}")]
        public async Task<IActionResult> GetSinglePet(string slug)
        {
            var query = new GetSinglePetQuery(slug);
            var validator = new GetSinglePetValidator();
            var validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Status = 400,
                    Detail = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                });
            }

            var pet = await _mediator.Send(query);
            return Ok(pet);
        }

        [HttpGet("service-list/{slug}")]
        public async Task<IActionResult> GetSingleService(string slug)
        {
            var query = new GetSingleServiceQuery(slug);
            var validator = new GetSingleServiceValidator();
            var validationResult = validator.Validate(query);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Status = 400,
                    Detail = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                });
            }

            var service = await _mediator.Send(query);
            return Ok(service);
        }

        [HttpPost("register-petpals")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { error = "Request body is missing or invalid." });
            }

            var validator = new RegisterValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var command = new RegisterCommand(request);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("login-petpals")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var validator = new LoginValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7807",
                    Title = "Login Error",
                    Status = 400,
                    Detail = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
                });
            }

            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Login gagal untuk email: {Email}", request.Email);
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7807",
                    Title = "Authentication Error",
                    Status = 401,
                    Detail = "Email atau password salah."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Terjadi kesalahan pada login.");
                return StatusCode(500, new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7807",
                    Title = "Login Error",
                    Status = 500,
                    Detail = "Terjadi kesalahan pada sistem."
                });
            }
        }

        [HttpGet("get-service-categories")]
        public async Task<IActionResult> GetAllServiceCategories([FromQuery] int? categoryId, [FromQuery] string? name)
        {
            try
            {
                var query = new GetAllServiceCategoriesQuery
                {
                    CategoryId = categoryId,
                    Name = name
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                Log.Error("Error: {Message}", ex.Message);
                return Problem(
                    type: "https://tools.ietf.org/html/rfc7807",
                    title: "Get Service Categories Error",
                    statusCode: (int)ex.StatusCode,
                    detail: ex.Message
                );
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error: {Message}", ex.Message);
                return Problem(
                    type: "https://tools.ietf.org/html/rfc7807",
                    title: "Unexpected Error",
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: "Terjadi kesalahan pada sistem."
                );
            }
        }

        [HttpGet("get-species")]
        public async Task<IActionResult> GetAllSpecies([FromQuery] int? speciesId, [FromQuery] string? name)
        {
            try
            {
                var query = new GetAllSpeciesQuery
                {
                    SpeciesId = speciesId,
                    Name = name
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                Log.Error("Error: {Message}", ex.Message);
                return Problem(
                    type: "https://tools.ietf.org/html/rfc7807",
                    title: "Get Species Error",
                    statusCode: (int)ex.StatusCode,
                    detail: ex.Message
                );
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error: {Message}", ex.Message);
                return Problem(
                    type: "https://tools.ietf.org/html/rfc7807",
                    title: "Unexpected Error",
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: "Terjadi kesalahan pada sistem."
                );
            }
        }


        [HttpGet("get-user-roles")]
        public async Task<IActionResult> GetAllUserRoles([FromQuery] GetAllUserRolesQuery query)
        {
            _logger.LogInformation("Processing GetAllUserRoles request...");

            try
            {
                var roles = await _mediator.Send(query);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user roles.");
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7807",
                    Title = "Server Error",
                    Status = 500,
                    Detail = "Terjadi kesalahan pada sistem."
                };
                return StatusCode(500, problemDetails);
            }
        }

        [HttpPost("adoptions-transaction")]
        public async Task<IActionResult> AdoptPet([FromBody] AdoptionTransactionRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

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

            try
            {
                var command = new AdoptionTransactionCommand(request.PetId, request.UserId);
                var result = await _mediator.Send(command);

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = "Adoption successful.",
                        adoptionId = result.AdoptionId
                    });
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    error = result.Message ?? "Adoption failed."
                });
            }
            catch (CustomException2 ex)
            {
                Log.Warning("Custom exception occurred: {Message}", ex.Message);
                return StatusCode(ex.StatusCode, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Error",
                    status = ex.StatusCode,
                    detail = ex.Message,
                    error_code = ex.ErrorCode
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred."
                });
            }
        }


        [HttpPost("service-transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] ServiceTransactionCommand serviceCommand)
        {
            var validationResult = await _serviceTransactionValidator.ValidateAsync(serviceCommand);
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

            try
            {
                var result = await _mediator.Send(serviceCommand);

                if (result.Success)
                {
                    return Ok(new
                    {
                        message = "Service transaction successful.",
                        transactionId = result.TransactionId
                    });
                }

                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    error = result.Message ?? "Transaction failed."
                });
            }
            catch (CustomException2 ex)
            {
                Log.Warning("Custom exception occurred: {Message}", ex.Message);
                return StatusCode(ex.StatusCode, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Error",
                    status = ex.StatusCode,
                    detail = ex.Message,
                    error_code = ex.ErrorCode
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error occurred.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred."
                });
            }
        }


    }

}

