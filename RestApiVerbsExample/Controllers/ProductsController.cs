using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiVerbsExample.Models;
using RestApiVerbsExample.Services;

namespace RestApiVerbsExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null) return NotFound();

            product.Id = id; // Ensure the ID is correct
            _productService.UpdateProduct(product);
            return NoContent();
        }

        // PATCH: api/products/{id}
        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id, [FromBody] Product patchData)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();

            if (patchData.Name != null)
            {
                product.Name = patchData.Name;
            }
            if (patchData.Price != 0)
            {
                product.Price = patchData.Price;
            }

            _productService.UpdateProduct(product);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null) return NotFound();

            _productService.DeleteProduct(id);
            return NoContent();
        }

        // OPTIONS: api/products
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, OPTIONS");
            return Ok();
        }

        // HEAD: api/products/{id}
        [HttpHead("{id}")]
        public IActionResult HeadProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok();
        }
    }
}
