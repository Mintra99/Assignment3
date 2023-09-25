namespace Assignment3.Data.Dtos.Franchises
{
    public class FranchiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int>? Movies { get; set; }
    }

}
