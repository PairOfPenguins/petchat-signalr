using petchat.Models;

namespace petchat.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllAsync();

        Task<Message?> GetByIdAsync(int id);
    }
}
