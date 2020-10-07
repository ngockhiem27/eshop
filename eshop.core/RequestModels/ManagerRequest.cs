using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eshop.core.RequestModels
{
    public class ManagerRequest
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("RoleId")]
        public int RoleId { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
