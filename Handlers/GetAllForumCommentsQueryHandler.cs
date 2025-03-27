using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Models;
using Serilog;

namespace PetPals_BackEnd_Group_9.Handlers
{
    
        public class GetAllForumCommentsQueryHandler : IRequestHandler<GetAllForumCommentsQuery, List<ForumCommentResponse>>
        {
            private readonly PetPalsDbContext _context;

            public GetAllForumCommentsQueryHandler(PetPalsDbContext context)
            {
                _context = context;
            }

            public async Task<List<ForumCommentResponse>> Handle(GetAllForumCommentsQuery request, CancellationToken cancellationToken)
            {
                Log.Information("Mencari komentar forum dengan filter: UserId={UserId}, CommentId={CommentId}",
                    request.UserId, request.CommentId);

                var query = _context.ForumComments.AsQueryable();

                if (request.UserId.HasValue)
                {
                    query = query.Where(fc => fc.UserId == request.UserId.Value);
                }

                if (request.CommentId.HasValue)
                {
                    query = query.Where(fc => fc.ForumCommentId == request.CommentId.Value);
                }

                var comments = await query
                    .Select(fc => new ForumCommentResponse
                    {
                        ForumCommentId = fc.ForumCommentId,
                        PostId = fc.PostId,
                        UserId = fc.UserId,
                        Comment = fc.Comment,
                        CreatedAt = fc.CreatedAt,
                        CreatedBy = fc.CreatedBy,
                        UpdatedAt = fc.UpdatedAt,
                        UpdatedBy = fc.UpdatedBy,
                        NameUser = fc.NameUser
                    })
                    .ToListAsync(cancellationToken);

                if (!comments.Any())
                {
                    Log.Warning("Tidak ada komentar forum yang ditemukan untuk filter yang diberikan.");
                }

                return comments;
            }
        }
    }

