namespace Assignment3.Data.Dtos.Movies
{
    public class MoviePostDto
    {
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
        public string? PictureUrl { get; set; }
        public string? TrailerUrl { get; set; }
    }
}