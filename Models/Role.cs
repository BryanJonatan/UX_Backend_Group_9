using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;
    }
}
