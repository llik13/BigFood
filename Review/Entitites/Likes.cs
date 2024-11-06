namespace Review.Entitites
{
    public class Likes
    {
        public int LikeId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User User { get; set; }
        public Reviews Review { get; set; }
    }

}
