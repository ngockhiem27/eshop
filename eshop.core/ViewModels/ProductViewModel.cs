using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class ProductViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("RegularPrice")]
        public decimal Regular_Price { get; set; }

        [JsonPropertyName("DiscountPrice")]
        public decimal Discount_Price { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime Created_At { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public DateTime Updated_At { get; set; }

        [JsonPropertyName("Categories")]
        public List<CategoryViewModel> Categories { get; set; }

        [JsonPropertyName("Images")]
        public List<ImageViewModel> Images { get; set; }

        public ProductViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }
    }
}
