using Microsoft.EntityFrameworkCore;
using System;

namespace PetPals_BackEnd_Group_9.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly PetPalsDbContext _context;

        public UserRepository(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new User { UserId = u.UserId, Name = u.Name })
                .FirstOrDefaultAsync();
        }
    }

}
