namespace PetPals_BackEnd_Group_9.Models
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
