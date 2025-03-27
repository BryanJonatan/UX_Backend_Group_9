using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetAllForumPostsQuery : IRequest<List<ForumPostResponse>>
    {
    }
}
