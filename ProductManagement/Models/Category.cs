using System.Text.Json.Serialization;

namespace ProductManagement.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //navigarion
        [JsonIgnore]
        public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
