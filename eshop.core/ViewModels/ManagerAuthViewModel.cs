﻿using eshop.core.JwtSettings;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class ManagerAuthViewModel
    {
        [JsonPropertyName("Manager")]
        public ManagerViewModel Manager { get; set; }

        [JsonPropertyName("JwtResult")]
        public JwtAuthResult JwtResult { get; set; }
    }
}
