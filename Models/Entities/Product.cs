using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPIManager.Models.Entities
{
    public class Product
    {

        [Range(100000, 999999, ErrorMessage = "ProductId must be a 6-digit number")]
        public required int ProductId { get; set; } //Unique identifier for the Product
        public required string Name { get; set; } //Name of the Product
        public string? Description { get; set; } //Description of the Product
        public required decimal Price { get; set; } // Price of the Product

        [Required(ErrorMessage = "Stock is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be a Negative value")]
        public required int Stock { get; set; } //Stock quantity
        public required DateTime CreatedAt { get; set; } //Date when the Product was Created
        public required DateTime UpdatedAt { get; set; } // Date when the Product was last Updated
    }
}
