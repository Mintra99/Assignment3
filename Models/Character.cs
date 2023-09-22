using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Alias { get; set; }

        public string Gender { get; set; }

        public string PictureUrl { get; set; }

        // Navigation property to relate characters to movies
        public ICollection<Movie> Movies { get; set; }
    }

}