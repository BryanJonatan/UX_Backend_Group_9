using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetPals_BackEnd_Group_9.Models
{
    [Table("forum_category")]
    public class ForumCategory
    {
        [Column("forum_category_id")]
        public int Id { get; set; }
        [Column("category_name")]
        public string? CategoryName { get; set; }
    }
}
