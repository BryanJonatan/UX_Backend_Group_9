namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumCommentResponse
    {
        public int ForumCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string NameUser { get; set; }
        public User User { get; set; }
    }
}
