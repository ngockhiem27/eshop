using eshop.core.ViewModels;
using System.Text.Json.Serialization;

namespace eshop.core.DTO.Response
{
    public class ManagerLoginResponse
    {
        [JsonPropertyName("manager")]
        public ManagerViewModel Manager { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
