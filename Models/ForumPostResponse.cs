namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumPostResponse
    {
        public int ForumPostId { get; set; }
        public int UserId { get; set; }
        public int ForumCategoryId { get; set; }
        public string CategoryName { get; set; } // Fetch from ForumCategory
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public User User { get; set; }
    }
}
