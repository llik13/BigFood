using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.DTO.Request
{
    public class ProductRequestDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }
    }
}
