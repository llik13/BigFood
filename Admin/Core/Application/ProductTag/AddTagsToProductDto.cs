using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.ProductTag
{
    public class AddTagsToProductDto
    {
        public int ProductId { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
    }
}
