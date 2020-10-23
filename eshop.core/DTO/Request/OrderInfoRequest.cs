using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.core.DTO.Request
{
    public class Item
    {
        [JsonPropertyName("ProductId")]
        public int ProductId { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
    public class OrderInfoRequest
    {
        [JsonPropertyName("Items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("CustomerId")]
        public int CustomerId { get; set; }

        public OrderInfoRequest()
        {
            Items = new List<Item>();
        }
    }
}
