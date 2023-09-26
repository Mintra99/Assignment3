namespace Assignment3.Data.Dtos.Movies
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public int? FranchiseId { get; set; }
        public List<int>? Characters { get; set; }
    }
}