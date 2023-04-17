using JokesWebApp.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JokesWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Joke> Jokes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserJokeComment> UserJokeComments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>()
            .HasIndex(r => new { r.JokeID, r.UserID })
            .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1BHDHEN\\SQLEXPRESS03;Database=JokesWebAppDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}