using System;
using System.Collections.Generic;

namespace eshop.core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public decimal Regular_Price { get; set; }

        public decimal Discount_Price { get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Updated_At { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public ProductViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }
    }
}
