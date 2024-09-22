using Newtonsoft.Json;
using RestApiVerbsExample.Models;

namespace RestApiVerbsExample.Services
{
    public class ProductService
    {
        private readonly string _filePath = "products.json";

        public List<Product> GetAllProducts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        }

        public Product GetProductById(int id)
        {
            var products = GetAllProducts();
            return products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            var products = GetAllProducts();
            products.Add(product);
            SaveProducts(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = GetAllProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                SaveProducts(products);
            }
        }

        public void DeleteProduct(int id)
        {
            var products = GetAllProducts();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                SaveProducts(products);
            }
        }

        private void SaveProducts(List<Product> products)
        {
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
