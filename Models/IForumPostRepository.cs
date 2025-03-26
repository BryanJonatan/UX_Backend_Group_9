namespace PetPals_BackEnd_Group_9.Models
{
    public interface IForumPostRepository
    {
        Task AddAsync(ForumPost forumPost);
    }

}
