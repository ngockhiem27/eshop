namespace eshop.core.Entities
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Item_Price { get; set; }
    }
}
