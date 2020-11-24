using eshop.core.ViewModels;

namespace eshop.webadmin.Services
{
    public interface IWebHookService
    {
        void NotifyNewProductAsync(ProductViewModel product);
        void NotifyUpdateProductAsync(int id, ProductViewModel product);
        void NotifyNewProductImageAsync(int id, ImageViewModel image);
        void NotifyRemoveProductAsync(int id);
    }
}