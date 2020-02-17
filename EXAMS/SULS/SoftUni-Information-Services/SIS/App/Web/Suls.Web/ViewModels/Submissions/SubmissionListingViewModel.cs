using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Submissions
{
    public class SubmissionListingViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int AchievedResult { get; set; }
        public int MaxPoints { get; set; }
        public string CreatedOn { get; set; }
    }
}
