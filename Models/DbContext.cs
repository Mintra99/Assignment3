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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            // https://learn.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }


        // Additional configuration and DbSet properties go here
    }

}
