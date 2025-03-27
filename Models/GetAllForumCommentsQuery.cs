using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllForumCommentsQuery : IRequest<List<ForumCommentResponse>>
    {
        public int? UserId { get; set; }
        public int? CommentId { get; set; }
    }
}
