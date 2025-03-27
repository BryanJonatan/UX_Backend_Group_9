using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetForumCommentsByPostIdQuery : IRequest<List<ForumCommentResponse>>
    {
        public int? PostId { get; set; }

        public GetForumCommentsByPostIdQuery(int? postId = null)
        {
            PostId = postId;
        }
    }
}
