namespace PetPals_BackEnd_Group_9.Models
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
    }
}
