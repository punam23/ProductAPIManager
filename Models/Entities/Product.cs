using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductAPIManager.Models.Entities
{
    public class Product
    {
        [JsonIgnore][Range(100000, 999999, ErrorMessage = "ProductId must be a 6-digit number")]
        public int ProductId { get; set; } //Unique identifier for the Product

        [Required(ErrorMessage = "Product Name is required")]
        public string? Name { get; set; } //Name of the Product
        public string? Description { get; set; } //Description of the Product

        [Required(ErrorMessage = "product Price is required")]
        [Range(1, 99999, ErrorMessage = "Price must be greater than 0 and less than 100000.")]
        public decimal Price { get; set; } = 0.0m; // Price of the Product

        [Required(ErrorMessage = "Stock is required")]
        [Range(1, 999, ErrorMessage = "Stock must be greater than 0 and less than 1000.")]
        public int Stock { get; set; } = 0; //Stock quantity

        [Required(ErrorMessage = "CreatedAt is required")]
        [JsonIgnore]
        public DateTime CreatedAt
        {
            get => _date;
            set
            {
                value = DateTime.Now;
            }
        } //Date when the Product was Created

        [Required(ErrorMessage = "UpdatedAt is required")]
        private DateTime _date = DateTime.Now;
        [JsonIgnore] public DateTime UpdatedAt
        {
                get => _date;
                set
                {
                    value = DateTime.Now;
                }
        } // Date when the Product was last Updated
    }
}
