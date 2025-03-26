using MediatR;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Validators;

namespace PetPals_BackEnd_Group_9.Command
{
    public class CreateForumPostCommand : IRequest<ForumPostResponse>
    {
        public int UserId { get; set; }
        public int ForumCategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
