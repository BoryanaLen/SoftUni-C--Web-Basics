using Suls.Data;
using Suls.Data.Models;
using Suls.Services.Contracts;
using System.Linq;

namespace Suls.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly SulsDbContext db;

        public ProblemsService(SulsDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
            };

            this.db.Problems.Add(problem);

            this.db.SaveChanges();
        }

        public IQueryable<Problem> GetAllProblems()
        {
            return this.db.Problems.AsQueryable();
        }

        public Problem GetProblemById(string id)
        {
            return this.db.Problems.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
