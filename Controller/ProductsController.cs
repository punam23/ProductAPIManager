using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ProductAPIManager.Data;
using ProductAPIManager.Models;
using ProductAPIManager.Models.Entities;
using System.Collections.Concurrent;

namespace ProductAPIManager.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, bool> GeneratedIds = new ConcurrentDictionary<int, bool>();
        private static readonly object LockObject = new object();
        private static int MinRange = 100000;
        private static int MaxRange = 999999;
        private DbContext context;
        private readonly ApplicationDbContext dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //api/products 
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(AddProductDto addProductDto)
        {
            int newId;

            lock (LockObject)
            {
                Random random = new Random();
                do
                {
                    newId = random.Next(MinRange, MaxRange); // Generate a random 6-digit number as per requirement
                }
                while (!GeneratedIds.TryAdd(newId, true)); // Ensure uniqueness by adding to the dictionary
            }

            //entities are separate from DTOs, by adding DTOs we achieve separation of concern
            //code resuability and generic
            var productEntity = new Product()
            {
                ProductId = newId,
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                Stock = addProductDto.Stock,
                CreatedAt = addProductDto.CreatedAt,
                UpdatedAt = addProductDto.UpdatedAt
            };

            dbContext.Products.Add(productEntity);
            await dbContext.SaveChangesAsync(); // save changes to db 

            return Ok(productEntity);
        }

        //api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await Task.FromResult(dbContext.Products.ToList());
            return Ok(allProducts);
        }

        //api/products/{id} 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            // Validate that the ID is a 6-digit number
            if (id < MinRange || id > MaxRange)
            {
                return BadRequest(new { Message = "Product ID must be a 6-digit postive number." });
            }

            var product = await Task.FromResult(dbContext.Products.Find(id));
            if(product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }
            return Ok(product);
        }
        
        //api/products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Validate that the ID is a 6-digit number
            if (id < MinRange || id > MaxRange)
            {
                return BadRequest(new { Message = "Product ID must be a 6-digit postive number." });
            }

            // Find the product by its ID
            var product = await Task.FromResult(dbContext.Products.Find(id));
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(); // save changes to db 
            return Ok(new { Message = "Product Deleted successfully." });
           
        }

        //api/products/{id} 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            // Validate that the ID is a 6-digit number
            if (id < MinRange || id > MaxRange)
            {
                return BadRequest(new { Message = "Product ID must be a 6-digit postive number." });
            }

            // Find the product by its ID
            var product = await Task.FromResult(dbContext.Products.Find(id));
            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            product.Description = updateProductDto.Description;
            product.Stock = updateProductDto.Stock;
            product.CreatedAt = updateProductDto.CreatedAt;
            product.UpdatedAt = updateProductDto.UpdatedAt;

            await dbContext.SaveChangesAsync(); // save changes to db 
            return Ok(product);

        }

        //api/products/increment-stock/{id}/{quantity} 
        [HttpPut("increment-stock/{id}/{quantity}")]
        public async Task<IActionResult> IncrementStock(int id, int quantity)
        {
            // Validate that the ID is a 6-digit number
            if (id < MinRange || id > MaxRange)
            {
                return BadRequest(new { Message = "Product ID must be a 6-digit postive number." });
            }

            if(quantity < 1)
            {
                return BadRequest(new { Message = "Quantity must be a positive number." });
            }

            // Find the product by its ID
            var product = await Task.FromResult(dbContext.Products.Find(id));

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            // Add the specified quantity to the stock
            product.Stock += quantity;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return Ok(new { Message = "Stock updated successfully.", ProductId = id, UpdatedStock = product.Stock });
        }

        //api/products/decrement-stock/{id}/{quantity} 
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            // Validate that the ID is a 6-digit number
            if (id < MinRange || id > MaxRange)
            {
                return BadRequest(new { Message = "Product ID must be a 6-digit postive number." });
            }

            if (quantity < 1)
            {
                return BadRequest(new { Message = "Quantity must be a positive number." });
            }

            // Find the product by its ID
            var product = await Task.FromResult(dbContext.Products.Find(id));

            if (product == null)
            {
                return NotFound(new { Message = "Product not found." });
            }

            // Ensure stock does not go below zero
            if (quantity > product.Stock)
            {
                return BadRequest(new { Message = "Insufficient stock.", ProductId = id, AvailableStock = product.Stock });
            }

            // Decrement the stock
            product.Stock -= quantity;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return Ok(new { Message = "Stock decremented successfully.", ProductId = id, UpdatedStock = product.Stock });
        }
    }
}



