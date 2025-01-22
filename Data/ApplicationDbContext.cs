using Microsoft.EntityFrameworkCore;
using petchat.Models;

namespace petchat.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; } 

    }
}
