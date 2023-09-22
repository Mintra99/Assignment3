using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Assignment3
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        // Additional configuration and DbSet properties go here
    }

}
