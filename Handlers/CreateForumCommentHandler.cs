using MediatR;
using PetPals_BackEnd_Group_9.Command;
using PetPals_BackEnd_Group_9.Models;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class CreateForumCommentHandler : IRequestHandler<CreateForumCommentCommand, ForumComment>
    {
        private readonly IForumCommentRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateForumCommentHandler(IForumCommentRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ForumComment> Handle(CreateForumCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var forumComment = new ForumComment
            {
                PostId = request.PostId,
                UserId = request.UserId,
                NameUser = user.Name,
                Comment = request.Comment,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = user.Name,
                UpdatedBy = user.Name,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _repository.AddAsync(forumComment);
            return forumComment;
        }
    }
}
