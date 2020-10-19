using eshop.core.JwtSettings;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class CustomerAuthViewModel
    {
        [JsonPropertyName("Customer")]
        public CustomerViewModel Customer { get; set; }

        [JsonPropertyName("JwtResult")]
        public JwtAuthResult JwtResult { get; set; }
    }
}
