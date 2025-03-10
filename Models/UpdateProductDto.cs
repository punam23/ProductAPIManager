using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductAPIManager.Models
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        public string? Name { get; set; } //Name of the Product
        public string? Description { get; set; } //Description of the Product

        [Required(ErrorMessage = "product Price is required")]
        [Range(1, 99999, ErrorMessage = "Price must be greater than 0 and less than 100000.")]
        public decimal Price { get; set; } = 0.0m; // Price of the Product

        [Required(ErrorMessage = "Stock is required")]
        [Range(1, 999, ErrorMessage = "Stock must be greater than 0 and less than 1000.")]
        public int Stock { get; set; } = 0; //Stock quantity
    }
}
