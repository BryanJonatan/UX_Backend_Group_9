using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumComment
    {
        [Key]
        public int ForumCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public ForumPost Post { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
