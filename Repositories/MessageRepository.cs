using Microsoft.EntityFrameworkCore;
using petchat.Data;
using petchat.Helpers;
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


        public async Task<List<Message>> GetAllAsync(QueryObject query)
        {
            var messagesQuery = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Content))
            {
                messagesQuery = messagesQuery.Where(s => s.Content.Contains(query.Content));
            }

            return await messagesQuery.ToListAsync();
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
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return null;
            }
            message.Content = updatemessage.Content;
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> DeleteAsync(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return null;
            }
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
