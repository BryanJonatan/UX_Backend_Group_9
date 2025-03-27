using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetForumCommentsByPostIdHandler : IRequestHandler<GetForumCommentsByPostIdQuery, List<ForumCommentResponse>>
    {
        private readonly PetPalsDbContext _context;

        public GetForumCommentsByPostIdHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ForumCommentResponse>> Handle(GetForumCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ForumComments.AsQueryable();

            if (request.PostId.HasValue)
            {
                query = query.Where(c => c.PostId == request.PostId.Value);
            }

            var comments = await query
                .Select(c => new ForumCommentResponse
                {
                    ForumCommentId = c.ForumCommentId,
                    PostId = c.PostId,
                    UserId = c.UserId,
                    Comment = c.Comment,
                    NameUser = c.NameUser,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return comments;
        }
    }
}
