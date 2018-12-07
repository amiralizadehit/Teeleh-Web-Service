using System.Data.Entity;
using System.Web.UI.WebControls;
using Teeleh.Models.Panel;


namespace Teeleh.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("DefaultConnection")
        {
                
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exchange>()
                .HasRequired(a => a.Advertisement)
                .WithMany(e=>e.ExchangeGames)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Advertisement>()
                .HasRequired(g=>g.LocationProvince)
                .WithMany(e=>e.Advertisements)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }

        // Teeleh 
        public DbSet<Admin> Admins { get; set; }

    }
}
