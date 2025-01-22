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

    }
}
