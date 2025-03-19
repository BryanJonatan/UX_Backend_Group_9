namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumPost
    {
        public int ForumPostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public object User { get; internal set; }
    }
}
