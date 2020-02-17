using Suls.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suls.Services.Contracts
{
    public interface ISubmissionsService
    {
        void CreateSubmission(string code, string problemId, string userId);

        IQueryable<Submission> GetAllSubmissionsByProblemId(string id);

        void DeleteSubmission(string id);

        Submission GetSubmissionById(string id);
    }
}
