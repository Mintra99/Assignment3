using System.ComponentModel.DataAnnotations;
using Assignment3.Models;

namespace Assignment3
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        public string Director { get; set; }

        public string PictureUrl { get; set; }

        public string TrailerUrl { get; set; }

        // Foreign Key to Franchise
        public int FranchiseId { get; set; }

        // Navigation property to relate movies to characters
        public ICollection<Character> Characters { get; set; }

        // Navigation property to relate movies to franchises
        public Franchise Franchise { get; set; }
    }
}
