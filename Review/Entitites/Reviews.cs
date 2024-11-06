namespace Review.Entitites
{
    public class Reviews
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

       /* 
        public User User { get; set; }
        public List<Likes> Likes { get; set; } = new List<Likes>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
       */
    }

}
