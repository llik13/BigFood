namespace Forum.DAL.Entities;

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
}