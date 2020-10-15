using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.core.DTO.Request
{
    public class ProductInfoRequest
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("RegularPrice")]
        public decimal RegularPrice { get; set; }

        [JsonPropertyName("DiscountPrice")]
        public decimal DiscountPrice { get; set; }

        [JsonPropertyName("Categories")]
        public List<CategoryViewModel> Categories { get; set; }
    }
}
