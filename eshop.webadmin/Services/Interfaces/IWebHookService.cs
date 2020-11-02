using eshop.core.ViewModels;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface IWebHookService
    {
        void PostProductUpdateAsync(ProductViewModel product);
    }
}