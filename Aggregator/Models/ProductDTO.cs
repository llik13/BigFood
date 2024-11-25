namespace Aggregator.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }

        
    }
}
