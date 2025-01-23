using petchat.Models;

namespace petchat.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllAsync();

        Task<Message?> GetByIdAsync(int id);
        Task<Message> CreateAsync(Message message);

        Task<Message> UpdateAsync(int id, Message message);

        Task<Message> DeleteAsync(int id);
    }
}
