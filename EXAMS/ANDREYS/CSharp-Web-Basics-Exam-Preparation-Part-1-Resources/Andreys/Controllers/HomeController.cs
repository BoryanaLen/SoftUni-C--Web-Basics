namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var products = this.productsService
                    .GetAllProducts()
                    .Select(x => new ProductListingViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        ImageUrl = x.ImageUrl
                    })
                    .ToList();

                return this.View(new PorductsAllViewModel { Products = products }, "Home");

                return this.View("Home");
            }
            else
            {
                return this.View();
            }
        }
    }
}
