using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class OrderItemsViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("OrderId")]
        public int Order_Id { get; set; }

        [JsonPropertyName("ProductId")]
        public int Product_Id { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("ItemPrice")]
        public decimal Item_Price { get; set; }
    }
}
