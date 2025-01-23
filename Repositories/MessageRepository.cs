using Microsoft.EntityFrameworkCore;
using petchat.Data;
using petchat.Interfaces;
using petchat.Models;

namespace petchat.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<Message>> GetAllAsync()
        {
           return await _context.Messages.ToListAsync();
        }

        public Task<Message?> GetByIdAsync(int id)
        {
            return _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Message> CreateAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> UpdateAsync(int id, Message updatemessage)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (message == null)
            {
                return null;
            }
            message.Content = updatemessage.Content;
            await _context.SaveChangesAsync();
            return message;
        }

    }
}
