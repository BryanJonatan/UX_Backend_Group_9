using MediatR;
using Microsoft.EntityFrameworkCore;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Helpers;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Validators;
using Serilog;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class CreateForumPostHandler : IRequestHandler<CreateForumPostCommand, ForumPostResponse>
    {
        private readonly PetPalsDbContext _context;
        private readonly IForumPostRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IForumCategoryRepository _categoryRepository;

        public CreateForumPostHandler(PetPalsDbContext context, IForumPostRepository repository, IUserRepository userRepository, IForumCategoryRepository categoryRepository)
        {
            _context = context;
            _repository = repository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ForumPostResponse> Handle(CreateForumPostCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Adding new forum post: Title = {Title}, Content = {Content}", request.Title, request.Content);

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                Log.Warning("User with ID {UserId} not found", request.UserId);
                throw new Exception("User not found");
            }

            var category = await _categoryRepository.GetByIdAsync(request.ForumCategoryId);
            if (category == null)
            {
                Log.Warning("Forum Category with ID {ForumCategoryId} not found", request.ForumCategoryId);
                throw new Exception("Category not found");
            }

            var forumPost = new ForumPost
            {
                UserId = request.UserId,
                ForumCategoryId = request.ForumCategoryId,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = user.Name,
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = user.Name,
                Slug = await SlugHelper.GenerateUniqueSlugAsync(request.Title, _context.ForumPosts),
            };

            await _repository.AddAsync(forumPost);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("Forum with title {PostTitle} added successfully by {UserName}", request.Title, user.Name);
            return new ForumPostResponse
            {
                ForumPostId = forumPost.ForumPostId,
                UserId = forumPost.UserId,
                ForumCategoryId = forumPost.ForumCategoryId,
                CategoryName = category.CategoryName,
                Title = forumPost.Title,
                Content = forumPost.Content,
                CreatedAt = forumPost.CreatedAt,
                CreatedBy = forumPost.CreatedBy,
                UpdatedAt = forumPost.UpdatedAt,
                UpdatedBy = forumPost.UpdatedBy,
                Slug = forumPost.Slug // ✅ Include in response
            };
        }
    }
}
