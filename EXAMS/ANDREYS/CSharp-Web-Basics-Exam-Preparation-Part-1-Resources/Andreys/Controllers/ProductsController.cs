using Andreys.Models;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add (ProductCreateViewModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Password must be at least 4 characters and at most 20");
            }

            if (model.Description.Length > 10)
            {
                return this.Error("Description must be at most 10");
            }

            if (model.Price < 0)
            {
                return this.Error("Price must be bigger than zero");
            }

            if (string.IsNullOrWhiteSpace(model.Category))
            {
                return this.Error("Category cannot be empty!");
            }

            if (string.IsNullOrWhiteSpace(model.Gender))
            {
                return this.Error("Gender cannot be empty!");
            }

            var category = (Category)Enum.Parse(typeof(Category), model.Category);

            var gender = (Gender)Enum.Parse(typeof(Gender), model.Gender);

            this.productsService.CreateProduct(model.Name, model.Description, model.ImageUrl, model.Price, category, gender);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var product = this.productsService.GetProductById(id);

            var viewModel = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = product.Category.ToString(),
                Gender = product.Gender.ToString(),
            };

            return this.View(viewModel);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.productsService.DeleteProduct(id);

            return this.Redirect("/");
        }
    }
}
