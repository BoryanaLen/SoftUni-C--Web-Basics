using Suls.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Services.Contracts
{
    public interface IProblemsService
    {
        IQueryable<Problem> GetAllProblems();

        void CreateProblem(string name, int points);

        Problem GetProblemById(string id);
    }
}
