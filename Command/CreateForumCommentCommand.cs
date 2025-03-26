using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class CreateForumCommentCommand : IRequest<ForumComment>
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
}
