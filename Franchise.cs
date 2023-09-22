using System.ComponentModel.DataAnnotations;

namespace Assignment3
{
    public class Franchise
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property to relate franchises to movies
        public ICollection<Movie> Movies { get; set; }
    }
}
