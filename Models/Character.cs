using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string? Alias { get; set; }

        public string Gender { get; set; } = null!;

        public string? PictureUrl { get; set; }

        // Navigation property to relate characters to movies
        public ICollection<Movie> Movies { get; set; } = null!;
    }

}