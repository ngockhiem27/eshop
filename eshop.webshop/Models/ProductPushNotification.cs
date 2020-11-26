using eshop.core.ViewModels;

namespace eshop.webshop.Models
{
    public class ProductPushNotification
    {
        public int NotificationType { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
