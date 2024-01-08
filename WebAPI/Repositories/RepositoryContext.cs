using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repositories.Config;

namespace WebAPI.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        { 
        
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfig());
        }
    }
}
