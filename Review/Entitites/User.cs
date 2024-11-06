namespace Review.Entitites
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Навигационное свойство для связей
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
        public List<Likes> Likes { get; set; } = new List<Likes>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
