using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleUserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; } 

        public string Password { get; set; } 

        public string Phone { get; set; }

        public string Address { get; set; } 

        public string Role { get; set; }
    }
}
