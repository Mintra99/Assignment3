using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Character
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [MaxLength(50)]
        public string? Alias { get; set; }

        [MaxLength(10)]
        public string Gender { get; set; } = null!;

        [MaxLength(2000)]
        public string? PictureUrl { get; set; }

        // Navigation property to relate characters to movies
        public ICollection<Movie>? Movies { get; set; }
    }

}