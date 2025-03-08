using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPIManager.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; } //Unique identifier for the Product
        public required string Name { get; set; } //Name of the Product
        public string? Description { get; set; } //Description of the Product
        public required decimal Price { get; set; } // Price of the Product
        public required int Stock { get; set; } //Stock quantity
        public required DateTime CreatedAt { get; set; } //Date when the Product was Created
        public required DateTime UpdatedAt { get; set; } // Date when the Product was last Updated
    }
}
