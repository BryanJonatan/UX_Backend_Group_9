using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetForumPostBySlugHandler : IRequestHandler<GetForumPostBySlugQuery, SingleForumPostResponse>
    {
        private readonly PetPalsDbContext _context;

        public GetForumPostBySlugHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<SingleForumPostResponse> Handle(GetForumPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.ForumPosts
                .Where(p => p.Slug == request.Slug)
                .Select(p => new SingleForumPostResponse
                {
                    ForumPostId = p.ForumPostId,
                    UserId = p.UserId,
                    Title = p.Title,
                    Content = p.Content,
                    Slug = p.Slug,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                Log.Warning("Forum post with slug {Slug} not found", request.Slug);
            }

            return post;
        }
    }
}
