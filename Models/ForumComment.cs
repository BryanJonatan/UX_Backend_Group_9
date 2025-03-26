using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    [Table("forum_comments")]
    public class ForumComment
    {
        [Column("forum_comment_id")]
        public int ForumCommentId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("created_by")]
        public string? CreatedBy { get; set; } = string.Empty;
        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("updated_by")]
        public string? UpdatedBy { get; set; } = string.Empty;
        [Column("name_user")]
        public string NameUser { get; set; }
        public User User { get; set; }
        public ForumPost Post { get; set; }
    }
}
