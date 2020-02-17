using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels.Products
{
    public class PorductsAllViewModel
    {
        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
