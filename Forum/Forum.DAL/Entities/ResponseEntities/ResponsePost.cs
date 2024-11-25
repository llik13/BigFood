namespace Forum.DAL.Entities;

public class ResponsePost
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    
    public int user_id { get; set; }
    public ResponseUser user { get; set; }
    
    public int category_id { get; set; }
    public ResponseCategory category { get; set; }
    
    public IEnumerable<ResponseComment> comments { get; set; }
}