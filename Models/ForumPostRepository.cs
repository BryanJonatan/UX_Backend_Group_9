using System;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumPostRepository : IForumPostRepository
    {
        private readonly PetPalsDbContext _context;

        public ForumPostRepository(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ForumPost forumPost)
        {
            await _context.ForumPosts.AddAsync(forumPost);
            await _context.SaveChangesAsync();
        }
    }
}
