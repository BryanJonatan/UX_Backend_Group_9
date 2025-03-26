using MediatR;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Command
{
    public class CreateForumPostCommand : IRequest<ForumPost>
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
