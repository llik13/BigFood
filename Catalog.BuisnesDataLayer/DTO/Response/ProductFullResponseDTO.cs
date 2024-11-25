using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.DTO.Response
{
    public class ProductFullResponseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double? Rating { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Availability { get; set; }
       
        public PromotionResponseDTO Promotion { get; set; }
        public TagResponseDTO Tag { get; set; }
    }
}
