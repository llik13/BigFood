namespace Forum.DAL.Entities;

public class ResponseComment
{
    public int id { get; set; }
    public string content { get; set; }
    public DateTime created_at { get; set; }
    
    public int user_id { get; set; }
    public User user { get; set; }
}