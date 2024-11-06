namespace Review.Entitites
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Навигационные свойства
        public User User { get; set; }
        public Reviews Review { get; set; }
    }
}
