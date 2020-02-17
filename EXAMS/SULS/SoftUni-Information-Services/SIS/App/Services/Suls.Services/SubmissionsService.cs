using Suls.Data;
using Suls.Data.Models;
using Suls.Services.Contracts;
using System;
using System.Linq;

namespace Suls.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly SulsDbContext db;
        private readonly IProblemsService problemsService;
        private readonly Random random;

        public SubmissionsService(SulsDbContext db, IProblemsService problemsService, Random random)
        {
            this.db = db;
            this.problemsService = problemsService;
            this.random = random;
        }

        public void CreateSubmission(string code, string problemId, string userId)
        {
            var problem = this.problemsService.GetProblemById(problemId);

            var submission = new Submission
            {
                Code = code,
                AchievedResult = random.Next(0, problem.Points + 1),
                CreatedOn = DateTime.UtcNow,
                ProblemId = problem.Id,
                UserId = userId,
            };

            this.db.Submissions.Add(submission);

            this.db.SaveChanges();
        }

        public void DeleteSubmission(string id)
        {
            var submission = this.db.Submissions.Where(x => x.Id == id).FirstOrDefault();

            this.db.Submissions.Remove(submission);

            this.db.SaveChanges();
        }

        public IQueryable<Submission> GetAllSubmissionsByProblemId(string id)
        {
            return this.db.Submissions
                .Where(x => x.ProblemId == id)
                .AsQueryable();
        }

        public Submission GetSubmissionById(string id)
        {
            return this.db.Submissions.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
