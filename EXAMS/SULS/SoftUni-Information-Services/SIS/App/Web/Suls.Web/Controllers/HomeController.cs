using SIS.HTTP;
using SIS.MvcFramework;
using Suls.Services.Contracts;
using Suls.Web.ViewModels.Problems;
using System.Linq;

namespace Suls.Web.Controllers
{
    class HomeController : Controller
    {
        private readonly IProblemsService problemsService;

        public HomeController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var problems = this.problemsService.GetAllProblems()
                    .Select(x => new ProblemsListingViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Count = x.Submissions.Count(),
                    }).ToList();

                return this.View(new ProblemsAllViewModel { Problems = problems}, "IndexLoggedIn");
            }
            else
            {
                return this.View();
            }
        }
    }
}
