using System;
using System.Collections.Generic;

namespace eshop.core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime Created_At { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public CategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}
