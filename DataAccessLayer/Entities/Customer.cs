namespace Catalog.DataAccessLayer.Entities
{
    public class Customer
    {
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public bool IsRowActive { get; set; }
        public IEnumerable<Delivery> deliveries { get; set; }
    }
}
