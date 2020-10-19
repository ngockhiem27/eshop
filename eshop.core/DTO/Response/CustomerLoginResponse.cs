using eshop.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eshop.core.DTO.Response
{
    public class CustomerLoginResponse
    {
        [JsonPropertyName("Customer")]
        public CustomerViewModel Customer { get; set; }

        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
