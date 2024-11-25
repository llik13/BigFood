using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.DTO.Response
{
    public class ReviewResponseDTO
    {
        public int ReviewId { get; set; }
        public byte Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }
    }
}
