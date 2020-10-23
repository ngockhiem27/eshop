using eshop.webshop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace eshop.webshop.Services
{
    public class CartService : ICartService
    {
        public CartService()
        {
        }

        public CartViewModel GetCart(HttpRequest httpRequest)
        {
            CartViewModel cart;
            if (httpRequest.Cookies.ContainsKey(AppConsts.CartCookieKey))
            {
                cart = JsonSerializer.Deserialize<CartViewModel>(httpRequest.Cookies[AppConsts.CartCookieKey]);
            }
            else
            {
                cart = new CartViewModel();
            }
            return cart;
        }

        public void SetCartCookie(CartViewModel cart, HttpResponse httpResponse)
        {
            CookieOptions option = new CookieOptions
            {
                HttpOnly = false,
                MaxAge = TimeSpan.FromDays(30)
            };
            httpResponse.Cookies.Append(AppConsts.CartCookieKey, JsonSerializer.Serialize(cart), option);
        }

        public void ClearCartCookie(HttpResponse httpResponse)
        {
            httpResponse.Cookies.Delete(AppConsts.CartCookieKey);
        }
    }
}
