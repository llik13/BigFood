namespace Aggregator.Models
{
    public class CatalogModel
    {
        public IEnumerable<ProductDTO>? Product { get; set; }
        public IEnumerable<CategoryDTO>? Category { get; set; }
    }
}
