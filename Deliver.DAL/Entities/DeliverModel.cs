using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.DAL.Entities
{
    public class DeliverModel
    {
        public string CourierId { get; set; }
        public string VehicleType { get; set; }
        public string? LicensePlate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
