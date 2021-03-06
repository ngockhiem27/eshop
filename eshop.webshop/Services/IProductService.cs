﻿using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IProductService
    {
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProduct();
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProductWithCategory();
        Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProductComplete();
        Task<(HttpStatusCode, List<ImageViewModel>)> GetProductImage(int id);
        Task<(HttpStatusCode, ProductViewModel)> GetProduct(int id);
    }
}