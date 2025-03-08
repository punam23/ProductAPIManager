# ProductAPIManager

**Description**

The ProductAPIManager is a RESTful web service designed to efficiently manage product inventories. It allows users to perform CRUD operations on products, manage stock levels, and ensure unique product identifiers. Built with ASP.NET Core and Entity Framework Core, this API is scalable, robust, and includes comprehensive unit tests to ensure functionality and reliability.

**Features**

          Product Management: Add, update, retrieve, delete products, increment or decrement stock levels with validation.
          
          Unique Product ID: Automatically generates a unique 6-digit ID for each product.
          
          Validation: Includes robust validation for stock levels and product ID formats.
          
          Error Handling: Provides meaningful error messages for invalid operations.
          
          Unit Tests: Comprehensive unit tests developed using xUnit and endpoint testings using Swagger
          
          In-Memory Database: Supports quick testing and development environments.
          
**Technologies Used**

          Framework: ASP.NET Core
          
          Database: Entity Framework Core (In-Memory Database for testing)
          
          Testing: xUnit for unit tests, Swagger for endpoints testing
          
          Serialization: Newtonsoft.Json for JSON parsing and handling
          
          IDE: Visual Studio
          
**Endpoints**

          Get All Products
          URL: GET /api/products
          Description: Retrieves a list of all available products.
          
          Get Product by ID
          URL: GET /api/products/{id}
          Description: Fetches the details of a specific product.
          
          Add Product
          URL: POST /api/products
          Description: Adds a new product to the database. Automatically assigns a unique 6-digit ID.
        
          Update Product
          URL: PUT /api/products/{id}
          Description: Updates the details of an existing product.
          
          Delete Product
          URL: DELETE /api/products/{id}
          Description: Removes a product from the database.
          
          Increment Stock
          URL: PUT /api/products/increment-stock/{id}/{quantity} 
          Description: Increases the stock level of a product by the specified quantity.
          
          Decrement Stock
          URL: PUT /api/products/decrement-stock/{id}/{quantity} 
          Description: Decreases the stock level of a product by the specified quantity, with validation for sufficient stock.

**How to Run**

          1. Clone the repository:
               git clone <repository-url>
          2. Open the solution in Visual Studio.
          3. Configure the connection string for SQL Server in appsettings.json:
          {
            "ConnectionStrings": {
              "DefaultConnection": "Data Source = ServerName;Initial Catalog = DatabaseName;User Id = UserId; Password = Password;Trusted_connection = true;TrustServerCertificate = true;"
            }
          }
          4. Apply migrations to set up the database:
               dotnet ef database update
          5. Build the solution to restore dependencies.
          6. Run the application.
          7. Test endpoints using tools like Postman or Swagger UI.
          


