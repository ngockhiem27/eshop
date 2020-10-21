using System.Collections.Generic;

namespace eshop.webshop.Models
{
    public class Item
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
    public class CartViewModel
    {
        public List<Item> Items { get; set; }

        public CartViewModel()
        {
            Items = new List<Item>();
        }
    }
}
