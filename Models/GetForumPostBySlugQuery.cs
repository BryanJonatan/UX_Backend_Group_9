using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public record GetForumPostBySlugQuery(string Slug) : IRequest<SingleForumPostResponse>;
}
