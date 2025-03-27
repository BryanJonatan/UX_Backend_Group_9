using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllForumPostsQuery : IRequest<List<ForumPostResponse>>
    {
        public int? ForumPostId { get; set; } // Optional search by Forum Post ID
        public int? ForumCategoryId { get; set; } // Optional search by Forum Category ID
        public string? Title { get; set; } // Optional search by Title

        public GetAllForumPostsQuery(int? forumPostId = null, int? forumCategoryId = null, string? title = null)
        {
            ForumPostId = forumPostId;
            ForumCategoryId = forumCategoryId;
            Title = title;
        }
    }
}
