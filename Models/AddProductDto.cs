using System.ComponentModel.DataAnnotations;

namespace ProductAPIManager.Models
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        public string? Name { get; set; } //Name of the Product
        public string? Description { get; set; } //Description of the Product

        [Required(ErrorMessage = "product Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be a Negative value")]
        public decimal Price { get; set; } // Price of the Product

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be a Negative value")]
        public int Stock { get; set; } //Stock quantity

        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; } //Date when the Product was Created

        [Required(ErrorMessage = "UpdatedAt is required")]
        public DateTime UpdatedAt { get; set; } // Date when the Product was last Updated
    }
}
