using Andreys.Data;
using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void CreateProduct(string name, string description, string imageUrl, decimal price, Category category, Gender gender)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                Category = category,
                Gender = gender,
            };

            this.db.Products.Add(product);

            this.db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = this.GetProductById(id);

            this.db.Products.Remove(product);

            this.db.SaveChanges();
        }

        public IQueryable<Product> GetAllProducts()
        {
            return this.db.Products.AsQueryable();
        }

        public Product GetProductById(int id)
        {
            return this.db.Products.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
