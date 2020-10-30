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
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
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
