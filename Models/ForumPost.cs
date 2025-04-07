using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    [Table("forum_posts")]
    public class ForumPost : ISluggable
    {
        [Column("forum_post_id")]
        public int ForumPostId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("name_user")]
        public string? UserName { get; set; } = string.Empty;
        [Column("title")]
        public string Title { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("created_by")]
        public string? CreatedBy { get; set; } = string.Empty;
        [Column("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        [Column("updated_by")]
        public string? UpdatedBy { get; set; } = string.Empty;
        [Column("forum_category_id")]
        public int ForumCategoryId { get; set; }
        [Column("slug")]
        public string Slug { get; set; }

        public ForumCategory ForumCategory { get; set; }
        public User User { get; set; }
    }
}
