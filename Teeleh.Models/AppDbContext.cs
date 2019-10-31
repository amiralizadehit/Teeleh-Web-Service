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

            //modelBuilder.Entity<Advertisement>().HasMany(m => m.Similars).WithMany();

            modelBuilder.Entity<Exchange>()
                .HasRequired(a => a.Advertisement)
                .WithMany(e=>e.ExchangeGames)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Advertisement>()
                .HasRequired(g=>g.LocationProvince)
                .WithMany(e=>e.Advertisements)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Notification>()
            //    .HasRequired(n => n.Advertisement)
            //    .WithMany(e => e.Notifications)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdBookmark>()
                .HasRequired(a=>a.Advertisement)
                .WithMany(e=>e.SavedByUsers)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<PSNAccountAdvertisement> PsnAccountAdvertisements { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<PSNAccountRequest> PSNAccountRequests { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<AdBookmark> AdBookmarks { get; set; }
        public DbSet<UserPhoneNumberValidator> UserPhoneNumberValidators { get; set; }
        

        // Teeleh 
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminSession> AdminSessions { get; set; }

    }
}
