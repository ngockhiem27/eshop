using System;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class RoleViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
