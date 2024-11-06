using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public partial class Promotion
    {
        public int PromotionId { get; set; }

        public int ProductId { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
