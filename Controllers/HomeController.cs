using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailStore.Data;
using RetailStore.Dto;
using RetailStore.Model;
using System.Collections.Generic;

namespace RetailStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        //variable declaration
        private readonly RetailDBContext _dbContext;

        //constructor initialization
        public HomeController(RetailDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet("GetCategories&Products")]

        public async Task<ActionResult<List<Category>>> GetCategory()
        {
            var category = _dbContext.Categories
                      .Include(p => p.Products)
                      .ToList();

            return Ok(category);

        }

        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                    CID = c.CID,
                    CategoryName = c.CategoryName
                })
                .ToList();

            return Ok(categories);
        }

        [HttpGet("GetProducts")]
      
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _dbContext.Products
                .Select(p => new ProductDTO
                {
                    PID = p.PID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryId = p.CategoryId // Optional, depending on your needs
                })
                .ToList();

            return Ok(products);
        }
        //public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        //{
        //    var products =  _dbContext.Products;
        //    return Ok(products);

        //}

        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            var productDto = new ProductDTO
            {
                PID = product.PID,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId // Optional
            };

            return Ok(productDto); // Return 200 OK with the DTO
        }

        //MAPPING

        private Category MapCategoryObject(CategoryDTO category)
        {
            var result = new Category();
           // result.CID = category.CID;
            result.CategoryName = category.CategoryName;
            result.Products = new List<Product>();
            category.Products.ForEach(o =>
            {
                var newProduct = new Product(); 
               
                newProduct.ProductName = o.ProductName; 
                newProduct.Quantity = o.Quantity;
                newProduct.Price = o.Price;
                result.Products.Add(newProduct);

            });

            return result;

        }

        [HttpPost("AddCategorywithproduct")]
        public async Task<ActionResult<Category>> AddCategoryProduct(CategoryDTO categoryDetail)
        {
            var newCategory = MapCategoryObject(categoryDetail);
            _dbContext.Categories.Add(newCategory);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = newCategory.CID}, newCategory);
        }

        [HttpPost("AddCategory")]

        public async Task<ActionResult<Category>> AddCategory(CategoryDTO category)
        {
            if (category == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            var newCategory = new Category { 
                CategoryName = category.CategoryName 
            };

            _dbContext.Categories.Add(newCategory);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddCategory), new { id = newCategory.CID }, newCategory); // Return 201 Created with the new category
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Product>> AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return NotFound(); // Return 404 if the product is not found
            }

            var newProduct = new Product
            {
                ProductName = productDTO.ProductName,
                Quantity = productDTO.Quantity,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId
            };

            _dbContext.Products.Add(newProduct);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddProduct), new { id = newProduct.PID }, newProduct);

        }

        [HttpDelete("DeleteCategories")]
        public async Task<ActionResult<Category>> DelCategory()
        {
            await _dbContext.Categories.ExecuteDeleteAsync();
            return Ok();

        }

        [HttpDelete("DeleteProducts")]
        public async Task<ActionResult<Product>> DelProduct()
        {
            await _dbContext.Products.ExecuteDeleteAsync();
            return Ok();

        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<Product>> DelProductById(int id)
        {
            await _dbContext.Products.Where(p => p.PID == id).ExecuteDeleteAsync();
                
            return Ok();

        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<Category>> DelCatById(int id)
        {
            await _dbContext.Categories.Where(p => p.CID == id).ExecuteDeleteAsync();

            return Ok();

        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<Category>> UpdateCatById(int id, CategoryDTO categoryDTO)
        {
            var toUpdate = await _dbContext.Categories.FindAsync(id);
            toUpdate.CategoryName = categoryDTO.CategoryName;
           

            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<Product>> UpdateProductById(int id, ProductDTO productDTO)
        {
            var toUpdate = await _dbContext.Products.FindAsync(id);
            toUpdate.ProductName = productDTO.ProductName;
            toUpdate.Price = productDTO.Price;
            toUpdate.Quantity = productDTO.Quantity;
            toUpdate.CategoryId = productDTO.CategoryId;

            await _dbContext.SaveChangesAsync();
            return Ok();

        }









    }
}
