using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);



            modelBuilder.Entity<Movie>()
                .HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "Harry Potter",
                    Description = "Harry Potter kills Voldemort",
                    Genre = GenreEnum.Action,
                    Year = 1970
                },
                new Movie()
                {
                    Id = 2,
                    Title = "The Fastest Sloth",
                    Description = "A sloth challenges the animal kingdom to a race, with unexpected results.",
                    Genre = GenreEnum.Comedy,
                    Year = 1995
                },
                    new Movie()
                    {
                        Id = 3,
                        Title = "Space Odyssey: The Return",
                        Description = "Explorers travel beyond the stars to discover a new galaxy.",
                        Genre = GenreEnum.Action,
                        Year = 2001
                    }
                );
        }
    }
}

