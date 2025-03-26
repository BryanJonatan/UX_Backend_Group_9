namespace PetPals_BackEnd_Group_9.Models
{
    public interface IForumCommentRepository
    {
        Task AddAsync(ForumComment comment);
        Task<ForumComment> GetByIdAsync(int id);
        Task<IEnumerable<ForumComment>> GetByPostIdAsync(int postId);
    }
}
