namespace ProductManagement.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductSummaryDto> Products { get; set; }
    }
}
