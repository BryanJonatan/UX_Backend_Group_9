using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;
using Serilog;
using System;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetAllForumPostsHandler : IRequestHandler<GetAllForumPostsQuery, List<ForumPostResponse>>
    {
        private readonly PetPalsDbContext _context;

        public GetAllForumPostsHandler(PetPalsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ForumPostResponse>> Handle(GetAllForumPostsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Fetching forum posts with parameters: ForumPostId = {ForumPostId}, Title = {Title}", request.ForumPostId, request.Title);

            var query = _context.ForumPosts
                .Include(fp => fp.ForumCategory)
                .Include(fp => fp.User)
                .AsQueryable();

            if (request.ForumPostId.HasValue)
            {
                query = query.Where(fp => fp.ForumPostId == request.ForumPostId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(fp => fp.Title.Contains(request.Title));
            }

            var forumPosts = await query
                .Select(fp => new ForumPostResponse
                {
                    ForumPostId = fp.ForumPostId,
                    UserId = fp.UserId,
                    ForumCategoryId = fp.ForumCategoryId,
                    CategoryName = fp.ForumCategory.CategoryName,
                    Title = fp.Title,
                    Content = fp.Content,
                    CreatedAt = fp.CreatedAt,
                    CreatedBy = fp.CreatedBy,
                    UpdatedAt = fp.UpdatedAt,
                    UpdatedBy = fp.UpdatedBy
                })
                .ToListAsync(cancellationToken);

            Log.Information("Fetched {Count} forum posts", forumPosts.Count);
            return forumPosts;
        }
    }
}
