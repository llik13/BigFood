namespace Aggregator.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
