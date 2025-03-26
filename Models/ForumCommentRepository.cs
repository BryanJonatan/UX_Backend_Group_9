using Microsoft.EntityFrameworkCore;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private readonly PetPalsDbContext _context;

        public ForumCommentRepository(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ForumComment comment)
        {
            await _context.ForumComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<ForumComment> GetByIdAsync(int id)
        {
            return await _context.ForumComments.FindAsync(id);
        }

        public async Task<IEnumerable<ForumComment>> GetByPostIdAsync(int postId)
        {
            return await _context.ForumComments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }
    }
}
