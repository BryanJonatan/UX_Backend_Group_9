using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly PetPalsDbContext _dbContext;
        private readonly IJwtService _jwtService;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(PetPalsDbContext dbContext, IJwtService jwtService, ILogger<LoginHandler> logger)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                _logger.LogWarning("Login gagal untuk email: {Email}", request.Email);
                throw new UnauthorizedAccessException("Email atau password salah.");
            }

            var token = _jwtService.GenerateToken(user);

            _logger.LogInformation("User {Email} berhasil login.", request.Email);

            return new LoginResponseDto { Token = token, Message = "Login berhasil", 
                User = new GetSingleUserResponse { 
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Phone = user.Phone,
                    Address = user.Address,
                    Role = user.Role.Name,
                } 
            };
        }
    }

}
