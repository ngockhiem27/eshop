using eshop.core.ViewModels;
using System.Text.Json.Serialization;

namespace eshop.infrastructure.WebPush.Models
{
    public class PushSubscriptionModel
    {
        [JsonPropertyName("Subscription")]
        public PushSubscription Subscription { get; set; }

        [JsonPropertyName("Customer")]
        public CustomerViewModel Customer { get; set; }
    }
}
