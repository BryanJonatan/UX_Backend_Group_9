namespace PetPals_BackEnd_Group_9.Models
{
    public class ForumComment
    {
        public int ForumCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public object Post { get; internal set; }
        public object User { get; internal set; }
    }
}
