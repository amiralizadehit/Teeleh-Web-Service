using System.Data.Entity;

namespace Teeleh.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("DefaultConnection")
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet <Image> Images { get; set; }

    }
}
