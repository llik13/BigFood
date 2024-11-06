namespace Map.Entities
{
    public class Deliver : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string number { get; set; }
    }
}
