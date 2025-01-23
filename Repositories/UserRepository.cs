using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using petchat.Data;
using petchat.DTOs.UserDTOs;
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

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(int id, UpdateUserDTO updateduser)
        {
            var user = await _context.Users.Include(m=>m.Messages).FirstOrDefaultAsync(u=>u.Id == id);
            if (user == null)
            {
                return null;
            }
            user.Username = updateduser.Username;
            await _context.SaveChangesAsync();
            return user;
            

        }
        public async Task<User> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == id);
            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;

        }
    }
}
