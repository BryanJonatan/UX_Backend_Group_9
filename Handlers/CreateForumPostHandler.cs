using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;
using PetPals_BackEnd_Group_9.Validators;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class CreateForumPostHandler : IRequestHandler<CreateForumPostCommand, ForumPostResponse>
    {
        private readonly IForumPostRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IForumCategoryRepository _categoryRepository;

        public CreateForumPostHandler(IForumPostRepository repository, IUserRepository userRepository, IForumCategoryRepository categoryRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ForumPostResponse> Handle(CreateForumPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var category = await _categoryRepository.GetByIdAsync(request.ForumCategoryId);
            if (category == null)
            {
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
                UpdatedBy = user.Name
            };

            await _repository.AddAsync(forumPost);

            return new ForumPostResponse
            {
                ForumPostId = forumPost.ForumPostId,
                UserId = forumPost.UserId,
                ForumCategoryId = forumPost.ForumCategoryId,
                CategoryName = category.CategoryName, // Fetch from ForumCategory
                Title = forumPost.Title,
                Content = forumPost.Content,
                CreatedAt = forumPost.CreatedAt,
                CreatedBy = forumPost.CreatedBy,
                UpdatedAt = forumPost.UpdatedAt,
                UpdatedBy = forumPost.UpdatedBy
            };
        }
    }
}
