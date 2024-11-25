namespace Catalog.DataAccessLayer.Entities
{
    public class Delivery
    {
        public int deliveryID { get; set; }
        public int customerId { get; set; }
        public int deliverId { get; set; }
        public string status { get; set; }
        public decimal cost { get; set; }
        public DateTime time { get; set; }
        public decimal distance { get; set; }
        public decimal startLatitude { get; set; }
        public decimal startLongitude { get; set; }
        public decimal endLongitude { get; set; }
        public decimal endLatitude { get; set; }
        public bool IsRowActive { get; set; }

        public Customer Customers { get; set; }
        public Deliver Deliver { get; set; }
    }
}
