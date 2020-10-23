using eshop.webshop.Models;
using Microsoft.AspNetCore.Http;

namespace eshop.webshop.Services
{
    public interface ICartService
    {
        CartViewModel GetCart(HttpRequest httpRequest);
        void SetCartCookie(CartViewModel cart, HttpResponse httpResponse);
        void ClearCartCookie(HttpResponse httpResponse);
    }
}