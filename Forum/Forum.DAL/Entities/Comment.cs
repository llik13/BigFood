namespace Forum.DAL.Entities;

public class Comment
{
    public int id { get; set; }
    public string content { get; set; }
    public DateTime created_at { get; set; }
    public int post_id { get; set; }
    public int user_id { get; set; }
    public User user { get; set; }
}