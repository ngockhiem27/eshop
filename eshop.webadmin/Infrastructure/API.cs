﻿namespace eshop.webadmin.Infrastructure
{
    public static class API
    {
        private const string serviceURI = "https://localhost:5001";
        private const string webhookURI = "https://localhost:7001";
        public static class Authenticate
        {
            public static string Login() => $"{serviceURI}/managerauth/login";
        }
        public static class Manager
        {
            public static string GetAllManager() => $"{serviceURI}/api/manager";
            public static string GetAllRole() => $"{serviceURI}/api/manager/role";
            public static string GetManagerById(int id) => $"{serviceURI}/api/manager/{id}";
            public static string AddManager() => $"{serviceURI}/api/manager/";
            public static string UpdateManager(int id) => $"{serviceURI}/api/manager/{id}";
            public static string DeleteManager(int id) => $"{serviceURI}/api/manager/{id}";
        }

        public static class Product
        {
            public static string GetAllProduct() => $"{serviceURI}/api/product";
            public static string GetAllProductWithCategory() => $"{serviceURI}/api/product/category";
            public static string AddProduct() => $"{serviceURI}/api/product";
            public static string UpdateProduct(int id) => $"{serviceURI}/api/product/{id}";
            public static string DeleteProduct(int id) => $"{serviceURI}/api/product/{id}";
            public static string GetProductImages(int id) => $"{serviceURI}/api/product/{id}/image";
            public static string AddProductImages(int id) => $"{serviceURI}/api/product/{id}/image";
            public static string DeleteProductImages(int imgId) => $"{serviceURI}/api/product/image/{imgId}";

        }

        public static class Category
        {
            public static string GetAllCategory() => $"{serviceURI}/api/category";
            public static string GetAllCategoryWithProduct() => $"{serviceURI}/api/category/product";
            public static string AddCategory() => $"{serviceURI}/api/category";
            public static string UpdateCategory(int id) => $"{serviceURI}/api/category/{id}";
            public static string DeleteCategory(int id) => $"{serviceURI}/api/category/{id}";
        }

        public static class Customer
        {
            public static string GetAllCustomers() => $"{serviceURI}/api/customer";
        }

        public static class Order
        {
            public static string GetAllOrders() => $"{serviceURI}/api/order";
        }

        public static class Webhook
        {
            public static string NewProduct() => $"hook/product";
            public static string UpdateProduct(int id) => $"hook/product/{id}";
            public static string RemoveProduct(int id) => $"hook/product/{id}";
            public static string NewProductImage(int id) => $"hook/product/{id}/image";
        }
    }
}
