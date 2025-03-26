using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BCrypt.Net;
namespace PetPals_BackEnd_Group_9.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly PetPalsDbContext _dbContext;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(PetPalsDbContext dbContext, ILogger<RegisterHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if email is already taken
            bool emailExists = await _dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (emailExists)
            {
                throw CreateProblemException("Email already registered.", HttpStatusCode.Conflict);
            }

            // Validate role existence
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);

            if (role == null)
            {
                throw CreateProblemException("Invalid RoleId.", HttpStatusCode.BadRequest);
            }

            // Hash the password securely using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                RoleId = request.RoleId,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = "SYSTEM",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = "SYSTEM"
            };

            // Use transaction to ensure atomicity
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("New user registered: {Email} with RoleId: {RoleId}", newUser.Email, newUser.RoleId);

                return new RegisterResponseDto
                {
                    UserId = newUser.UserId,
                    Name = newUser.Name,
                    Email = newUser.Email,
                    Role = role.Name,
                    Address = newUser.Address,
                    City = newUser.City
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error registering user.");
                throw CreateProblemException("Registration failed. Please try again.", HttpStatusCode.InternalServerError);
            }
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static Exception CreateProblemException(string detail, HttpStatusCode statusCode)
        {
            var problemDetails = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title = "Registration Error",
                status = (int)statusCode,
                detail
            };
            return new Exception(JsonSerializer.Serialize(problemDetails));
        }
    }
}
