namespace Assignment3.Data.Dtos.Characters
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Alias { get; set; }
        public string Gender { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public List<int>? Movies { get; set; }
    }
}