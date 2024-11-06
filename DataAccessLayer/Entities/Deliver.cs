namespace Catalog.DataAccessLayer.Entities
{
    public class Deliver
    {
        public int deliverId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string number { get; set; }
        public bool IsRowActive { get; set; }

        IEnumerable<Delivery> Deliveries { get; set; }
    }
}
