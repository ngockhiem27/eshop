﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class CategoryViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime Created_At { get; set; }

        [JsonPropertyName("Products")]
        public List<ProductViewModel> Products { get; set; }

        public CategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}
