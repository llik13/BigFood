namespace Forum.DAL.Entities;

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    
    public int user_id { get; set; }
    public User user { get; set; }
    
    public int category_id { get; set; }
    public Category category { get; set; }
    
    public IEnumerable<Comment> comments { get; set; }
}