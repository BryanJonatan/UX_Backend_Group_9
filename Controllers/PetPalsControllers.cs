﻿using MediatR;
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
using Microsoft.EntityFrameworkCore;

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
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
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
                    Detail = "Incorrect email or password."
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
                var command = new AdoptionTransactionCommand(request.PetId, request.AdopterId, request.OwnerId);
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

        [HttpGet("transaction-history/{adopterId}")]
        public async Task<IActionResult> GetTransactionHistory(int adopterId, string transactionType = "All")
        {
            try
            {
                _logger.LogInformation("Processing transaction history request for AdopterId: {AdopterId}", adopterId);

                var query = new TransactionHistoryQuery { AdopterId = adopterId, TransactionType = transactionType };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (ApiException ex)
            {
                _logger.LogWarning("Error fetching transaction history for AdopterId: {AdopterId}: {Message}", adopterId, ex.Message);
                return StatusCode((int)ex.StatusCode, ex.ToProblemDetails(HttpContext));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching transaction history for AdopterId: {AdopterId}", adopterId);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiException(HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message).ToProblemDetails(HttpContext));
            }
        }

        [HttpGet("adoption-transaction-detail/{adoptionId}")]
        public async Task<IActionResult> GetSingleAdoptionTransaction(int adoptionId)
        {
            try
            {
                var query = new GetSingleAdoptionTransactionQuery(adoptionId);
                var post = await _mediator.Send(query);

                if (post == null)
                {
                    return NotFound(new
                    {
                        type = "https://tools.ietf.org/html/rfc7807",
                        title = "Not Found",
                        status = 404,
                        detail = $"Adoption transaction with id '{adoptionId}' not found.",
                        instance = HttpContext.Request.Path
                    });
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving adoption transaction with id {AdoptionId}", adoptionId);

                return StatusCode(500, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred while retrieving the adoption transaction.",
                    instance = HttpContext.Request.Path
                });
            }
        }


        [HttpGet("service-transaction-detail/{transactionId}")]
        public async Task<IActionResult> GetSingleServiceTransaction(int transactionId)
        {
            try
            {
                var query = new GetSingleServiceTransactionQuery(transactionId);
                var post = await _mediator.Send(query);

                if (post == null)
                {
                    return NotFound(new
                    {
                        type = "https://tools.ietf.org/html/rfc7807",
                        title = "Not Found",
                        status = 404,
                        detail = $"Service transaction with id '{transactionId}' not found.",
                        instance = HttpContext.Request.Path
                    });
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving service transaction with id {TransactionId}", transactionId);

                return StatusCode(500, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred while retrieving the service transaction.",
                    instance = HttpContext.Request.Path
                });
            }
        }

        [HttpGet("adoption-transaction-request/{ownerId}")]
        public async Task<IActionResult> GetAdoptionTransactionRequest(int ownerId)
        {
            try
            {
                _logger.LogInformation("Processing adoption transaction request for OwnerId: {OwnerId}", ownerId);

                var query = new AdoptionTransactionRequestQuery { OwnerId = ownerId };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (ApiException ex)
            {
                _logger.LogWarning("Error fetching adoption transaction request for OwnerId: {OwnerId}: {Message}", ownerId, ex.Message);
                return StatusCode((int)ex.StatusCode, ex.ToProblemDetails(HttpContext));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching adoption transaction request for OwnerId: {OwnerId}", ownerId);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiException(HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message).ToProblemDetails(HttpContext));
            }
        }

        [HttpGet("service-transaction-request/{providerId}")]
        public async Task<IActionResult> GetServiceTransactionRequest(int providerId)
        {
            try
            {
                _logger.LogInformation("Processing service transaction request for ProviderId: {ProviderId}", providerId);

                var query = new ServiceTransactionRequestQuery { ProviderId = providerId };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (ApiException ex)
            {
                _logger.LogWarning("Error fetching service transaction request for ProviderId: {ProviderId}: {Message}", providerId, ex.Message);
                return StatusCode((int)ex.StatusCode, ex.ToProblemDetails(HttpContext));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching service transaction request for ProviderId: {ProviderId}", providerId);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiException(HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message).ToProblemDetails(HttpContext));
            }
        }

        [HttpPost("forum-post")]
        public async Task<IActionResult> CreateForumPost([FromBody] CreateForumPostCommand command)
        {
            var validator = new CreateForumPostValidator();
            var validationResult =await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateForumPost), new { id = result.ForumPostId }, result);
        }

        [HttpPost("forum-comment")]
        public async Task<IActionResult> CreateForumComment([FromBody] CreateForumCommentCommand command)
        {
            var validationResult = new CreateForumCommentValidator().Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Validation Error",
                    status = 400,
                    errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateForumComment), new { id = result.ForumCommentId }, result);
        }

        [HttpGet("get-all-forum-categories")]
        public async Task<IActionResult> GetForumCategories([FromQuery] int? forumCategoryId)
        {
            var query = new GetForumCategoriesQuery(forumCategoryId);
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

        [HttpGet("get-all-forum-posts")]
        public async Task<ActionResult<List<ForumPostResponse>>> GetForumPosts([FromQuery] int? forumPostId, [FromQuery] int? forumCategoryId, [FromQuery] string? title)
        {
            var query = new GetAllForumPostsQuery(forumPostId, forumCategoryId, title);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-all-forum-posts/{slug}")]
        public async Task<IActionResult> GetForumPostBySlug(string slug)
        {
            try
            {
                var query = new GetForumPostBySlugQuery(slug);
                var post = await _mediator.Send(query);

                if (post == null)
                {
                    return NotFound(new
                    {
                        type = "https://tools.ietf.org/html/rfc7807",
                        title = "Not Found",
                        status = 404,
                        detail = $"Forum post with slug '{slug}' not found.",
                        instance = HttpContext.Request.Path
                    });
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving forum post with slug {Slug}", slug);

                return StatusCode(500, new
                {
                    type = "https://tools.ietf.org/html/rfc7807",
                    title = "Internal Server Error",
                    status = 500,
                    detail = "An unexpected error occurred while retrieving the forum post.",
                    instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPost("input-new-pets")]
        public async Task<IActionResult> AddPet([FromBody] AddPetCommand command)
        {
            var validator = new AddPetCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(AddPet), new { id = result.PetId }, result);
        }

        [HttpPut("edit-pet/{petId}")]
        public async Task<IActionResult> EditPet([FromBody] EditPetsCommand command)
        {
            var validator = new EditPetCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var result = await _mediator.Send(command);

            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(new { status = result.Status, title = result.Title, detail = result.Detail });
            }
            return Problem(statusCode: (int)result.Status, title: result.Title, detail: result.Detail);
        }

        [HttpDelete("remove-pet/{petId}")]
        public async Task<IActionResult> RemovePet(int petId)
        {
            var query = new DeletePetQuery(petId);
            var result = await _mediator.Send(query);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(new { status = result.Status, title = result.Title, detail = result.Detail });
            }
            return Problem(statusCode: (int)result.Status, title: result.Title, detail: result.Detail);
        }

        [HttpPost("input-new-services")]
        public async Task<IActionResult> AddService([FromBody] AddServiceCommand command)
        {
            var validator = new AddServiceCommandValidator();
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(AddService), new { id = result.ServiceId }, result);
        }

        [HttpPut("edit-service/{serviceId}")]
        public async Task<IActionResult> EditService([FromBody] EditServiceCommand command)
        {
            var validator = new EditServiceCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            var result = await _mediator.Send(command);

            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(new { status = result.Status, title = result.Title, detail = result.Detail });
            }
            return Problem(statusCode: (int)result.Status, title: result.Title, detail: result.Detail);
        }

        [HttpDelete("remove-service/{serviceId}")]
        public async Task<IActionResult> RemoveService(int serviceId)
        {
            var query = new DeleteServiceQuery(serviceId);
            var result = await _mediator.Send(query);
            if (result.Status == HttpStatusCode.OK)
            {
                return Ok(new { status = result.Status, title = result.Title, detail = result.Detail });
            }
            return Problem(statusCode: (int)result.Status, title: result.Title, detail: result.Detail);
        }

        //[HttpGet("get-all-forum-comments")]
        //public async Task<IActionResult> GetAllForumComments([FromQuery] int? userId, [FromQuery] int? commentId)
        //{
        //    try
        //    {
        //        Log.Information("Memproses permintaan GET forum comments dengan userId={UserId} dan commentId={CommentId}",
        //            userId, commentId);

        //        var query = new GetAllForumCommentsQuery
        //        {
        //            UserId = userId,
        //            CommentId = commentId
        //        };

        //        var comments = await _mediator.Send(query);

        //        return Ok(comments);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, "Terjadi kesalahan saat mengambil forum comments.");
        //        return Problem(
        //            title: "Internal Server Error",
        //            detail: "Terjadi kesalahan pada server.",
        //            statusCode: StatusCodes.Status500InternalServerError
        //        );
        //    }
        //}

        [HttpGet("get-all-comment/{postId}")]
        public async Task<IActionResult> GetForumCommentsByPostId(int postId)
        {
            var query = new GetForumCommentsByPostIdQuery(postId);
            var comments = await _mediator.Send(query);
            return Ok(comments);
        }

        [HttpGet("get-owner-pets/{ownerId}")]
        public async Task<IActionResult> GetOwnerPets(int ownerId)
        {
            var query = new GetOwnerPetsQuery(ownerId);
            var validator = new GetOwnerPetsValidator();
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

            var pets = await _mediator.Send(query);
            return Ok(pets);
        }

        [HttpGet("get-provider-services/{providerId}")]
        public async Task<IActionResult> GetProviderServices(int providerId)
        {
            var query = new GetProviderServicesQuery(providerId);
            var validator = new GetProviderServicesValidator();
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

            var services = await _mediator.Send(query);
            return Ok(services);
        }


    }

}

