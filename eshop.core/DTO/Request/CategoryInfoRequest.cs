using System.Text.Json.Serialization;

namespace eshop.core.DTO.Request
{
    public class CategoryInfoRequest
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
