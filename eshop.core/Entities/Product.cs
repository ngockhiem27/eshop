using System;

namespace eshop.core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public decimal Regular_Price { get; set; }

        public decimal Discount_Price { get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Updated_At { get; set; }
    }
}
