using System;

namespace eshop.core.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Product_Id { get; set; }

        public String Product_Name { get; set; }

        public int Category_Id { get; set; }

        public String Category_Name { get; set; }

        public DateTime Category_Created_At { get; set; }

        public decimal Regular_Price { get; set; }

        public decimal Discount_Price { get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Updated_At { get; set; }
    }
}
