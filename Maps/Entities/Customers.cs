namespace Map.Entities
{
    public class Customers : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string email { get; set; }

        public IEnumerable<Delivery> deliveries { get; set; }

    }
}
