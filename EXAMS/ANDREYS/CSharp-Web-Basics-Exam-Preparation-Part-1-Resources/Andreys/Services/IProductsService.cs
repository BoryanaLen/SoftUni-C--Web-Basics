using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Andreys.Services
{
    public interface IProductsService
    {
        IQueryable<Product> GetAllProducts();

        void CreateProduct(string name, string description, string imageUrl, decimal price, Category category, Gender gender);

        Product GetProductById(int id);

        void DeleteProduct(int id);
    }
}
