using eTickets.IdentityEntities;
using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(am => am.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartMovies)
                .WithOne(cm => cm.Cart)
                .HasForeignKey(cm => cm.CartId);

            modelBuilder.Entity<CartMovie>()
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.CartMovies)
                .HasForeignKey(cm => cm.MovieId);

            modelBuilder.Entity<CartMovie>()
                .HasOne(cm => cm.Cart)
                .WithMany(c => c.CartMovies)
                .HasForeignKey(cm => cm.CartId);

            // Specify precision and scale for Subtotal property in CartMovie
            modelBuilder.Entity<CartMovie>()
                .Property(cm => cm.Subtotal)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Cart> Cats { get;set; }
        public DbSet<CartMovie> CartMovies { get; set; }
    }
}
