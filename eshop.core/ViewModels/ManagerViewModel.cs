using System;
using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class ManagerViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("RoleId")]
        public int Role_Id { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonIgnore]
        public string Password_Hash { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime Created_At { get; set; }

        [JsonPropertyName("RoleName")]
        public string Role_Name { get; set; }
    }
}
