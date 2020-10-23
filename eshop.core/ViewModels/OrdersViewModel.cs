using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class OrdersViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("CustomerId")]
        public int Customer_Id { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime Created_At { get; set; }
        [JsonPropertyName("Total")]
        public decimal Total { get; set; }
        [JsonPropertyName("OrderItems")]
        public List<OrderItemsViewModel> OrderItems { get; set; }
        public OrdersViewModel()
        {
            OrderItems = new List<OrderItemsViewModel>();
        }
    }
}
