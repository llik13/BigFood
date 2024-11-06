namespace Map.Entities
{
    public class Delivery : BaseEntity
    {
        public int customerId { get; set; }
        public string status { get; set; }
        public double cost { get; set; }
        public DateTime time { get; set; }
        public decimal distance { get; set; }
        public decimal startLatitude { get; set; }
        public decimal startLongitude { get; set; }
        public decimal endLongitude { get; set; }
        public decimal endLatitude { get; set; }
    }
}
