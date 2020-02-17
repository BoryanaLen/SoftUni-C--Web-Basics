using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Problems
{
    public class ProblemsAllViewModel
    {
        public IEnumerable<ProblemsListingViewModel> Problems { get; set; }
    }
}
