using SIS.HTTP;
using SIS.MvcFramework;
using Suls.Services.Contracts;
using Suls.Web.ViewModels.Problems;
using Suls.Web.ViewModels.Submissions;
using System.Linq;

namespace Suls.Web.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public ProblemsController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, int points)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name))
            {
                return this.Error("Invalid name");
            }

            if (points <= 0 || points > 100)
            {
                return this.Error("Points range: [1-100]");
            }

            this.problemsService.CreateProblem(name, points);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            var problem = this.problemsService.GetProblemById(id);

            var submissions = this.submissionsService.GetAllSubmissionsByProblemId(id)
                .Select(x => new SubmissionListingViewModel
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn.ToString(),
                    AchievedResult = x.AchievedResult,
                    MaxPoints = problem.Points,
                    Username = x.User.Username,
                });

            var viewModel = new ProblemDetailsViewModel
            {
                Id = id,
                Name = problem.Name,
                Submissions = submissions,
            };

            return this.View(viewModel);
        }
    }
}
