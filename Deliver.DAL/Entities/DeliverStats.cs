using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL.Entities
{
    public class DeliverStats
    {
        public int CourierStatsId { get; set; } 
        public string CourierId { get; set; }
        public int DeliveredOrders { get; set; } = 0;
        public TimeSpan AverageTime { get; set; } = TimeSpan.Zero;
        public decimal Rating { get; set; } = 0.0M;
    }
}
