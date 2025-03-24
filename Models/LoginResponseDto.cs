namespace PetPals_BackEnd_Group_9.Models
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public GetSingleUserResponse User { get; set; }
    }
}
