using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class CreateForumPostHandler : IRequestHandler<CreateForumPostCommand, ForumPost>
    {
        private readonly IForumPostRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateForumPostHandler(IForumPostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ForumPost> Handle(CreateForumPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var forumPost = new ForumPost
            {
                UserId = request.UserId,
                UserName = user.Name,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = user.Name, // Automatically setting CreatedBy
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = user.Name,
               
            };

            await _repository.AddAsync(forumPost);
            return forumPost;
        }
    }
}
