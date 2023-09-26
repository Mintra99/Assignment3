using System.ComponentModel.DataAnnotations;

namespace Assignment3.Data.Dtos.Characters
{
    public class CharacterPutDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FullName { get; set; } = null!;
        [StringLength(50)]
        public string? Alias { get; set; }
        public string Gender { get; set; } = null!;
        public string? PictureUrl { get; set; }
    }
}
