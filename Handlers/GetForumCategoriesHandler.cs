using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class GetForumCategoriesHandler : IRequestHandler<GetForumCategoriesQuery, List<ForumCategoryResponse>>
    {
        private readonly PetPalsDbContext _context;
        private readonly ILogger<GetForumCategoriesHandler> _logger;

        public GetForumCategoriesHandler(PetPalsDbContext context, ILogger<GetForumCategoriesHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ForumCategoryResponse>> Handle(GetForumCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ForumCategory> query = _context.ForumCategories;

            if (request.ForumCategoryId.HasValue)
            {
                query = query.Where(fc => fc.Id == request.ForumCategoryId.Value);
            }

            var categories = await query
                .Select(fc => new ForumCategoryResponse(
                    fc.Id,
                    fc.CategoryName // ✅ Use `CategoryName` instead of `Name`
                ))
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Retrieved {Count} forum categories", categories.Count);
            return categories;
        }
    }


}
