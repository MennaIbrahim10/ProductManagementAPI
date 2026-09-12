using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProductManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 10000)]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        //Navigation
        [JsonIgnore]
        public Category ? category { get; set; }
    }
}
