using petchat.DTOs.UserDTOs;
using petchat.Models;

namespace petchat.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id); 
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(int id, UpdateUserDTO user);
        Task<User> DeleteAsync(int id);
        Task<bool> UserExists(string username);

        Task<User> GetByUsernameAsync(string username);


    }
}
