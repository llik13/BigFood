using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class ProductTag
    {
        public int TagId { get; set; }

        public string TagName { get; set; } = null!;

        public virtual ICollection<Product> Product { get; set; } = new List<Product>();
    }
}
