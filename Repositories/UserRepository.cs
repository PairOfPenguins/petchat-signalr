using Microsoft.EntityFrameworkCore;
using petchat.Data;
using petchat.Interfaces;
using petchat.Models;

namespace petchat.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(x => x.Messages).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(x => x.Messages).FirstOrDefaultAsync(u => u.Id == id);

        }
    }
}
