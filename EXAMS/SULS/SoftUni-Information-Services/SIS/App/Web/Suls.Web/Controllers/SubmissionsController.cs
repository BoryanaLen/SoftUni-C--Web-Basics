using SIS.HTTP;
using SIS.MvcFramework;
using Suls.Services.Contracts;
using Suls.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;

        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var problem = this.problemsService.GetProblemById(id);

            if (problem == null)
            {
                return this.Error("Problem not found!");
            }

            var viewModel = new SubmissionViewModel
            {
                ProblemId = problem.Id,
                Name = problem.Name,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string code, string problemId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (code == null || code.Length < 30)
            {
                return this.Error("Please provide code with at least 30 characters.");
            }

            this.submissionsService.CreateSubmission(code, problemId, this.User);

            return Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            string problemId = this.submissionsService.GetSubmissionById(id).ProblemId;

            this.submissionsService.DeleteSubmission(id);

            return this.Redirect($"/Problems/Details?id={problemId}");
        }
    }
}
