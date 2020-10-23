using System;

namespace eshop.core.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public DateTime Created_At { get; set; }
        public decimal Total { get; set; }
    }
}
