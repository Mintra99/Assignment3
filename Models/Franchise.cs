using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Franchise
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property to relate franchises to movies
        public ICollection<Movie> Movies { get; set; } = null!;
    }
}
