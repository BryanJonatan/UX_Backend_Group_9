namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumCategoryRepository : IForumCategoryRepository
    {
        private readonly PetPalsDbContext _context;

        public ForumCategoryRepository(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<ForumCategory> GetByIdAsync(int categoryId)
        {
            return await _context.ForumCategories.FindAsync(categoryId);
        }
    }
}
