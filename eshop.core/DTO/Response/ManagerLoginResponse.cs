﻿using eshop.core.ViewModels;
using System.Text.Json.Serialization;

namespace eshop.core.DTO.Response
{
    public class ManagerLoginResponse
    {
        [JsonPropertyName("Manager")]
        public ManagerViewModel Manager { get; set; }

        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
