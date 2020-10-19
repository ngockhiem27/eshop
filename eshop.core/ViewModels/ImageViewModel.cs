using System.Text.Json.Serialization;

namespace eshop.core.ViewModels
{
    public class ImageViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("ProductId")]
        public int Product_Id { get; set; }

        [JsonPropertyName("FilePath")]
        public string File_Path { get; set; }
    }
}
