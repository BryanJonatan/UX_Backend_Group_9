using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetAllForumPostsHandler : IRequestHandler<GetAllForumPostsQuery, List<ForumPostResponse>>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<GetAllForumPostsHandler> _logger;

        public GetAllForumPostsHandler(PetPalsDbContext context, ILogger<GetAllForumPostsHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ForumPostResponse>> Handle(GetAllForumPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.ForumPosts
                .Include(fp => fp.ForumCategory) // Include category details
                .Select(fp => new ForumPostResponse
                {
                    ForumPostId = fp.ForumPostId,
                    UserId = fp.UserId,
                    ForumCategoryId = fp.ForumCategoryId,
                    CategoryName = fp.ForumCategory != null ? fp.ForumCategory.CategoryName ?? "Uncategorized" : "Uncategorized",
                    Title = fp.Title,
                    Content = fp.Content,
                    CreatedAt = fp.CreatedAt,
                    CreatedBy = fp.CreatedBy ?? "System",
                    UpdatedAt = fp.UpdatedAt,
                    UpdatedBy = fp.UpdatedBy ?? "System"
                })
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} forum posts", posts.Count);
            return posts;
        }
    }
}
