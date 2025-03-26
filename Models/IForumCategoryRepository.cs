namespace PetPals_BackEnd_Group_9.Models
{
    public interface IForumCategoryRepository
    {
        Task<ForumCategory> GetByIdAsync(int categoryId);
    }
}
