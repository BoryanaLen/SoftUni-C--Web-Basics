using Suls.Web.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubmissionListingViewModel> Submissions { get; set; }
    }
}
