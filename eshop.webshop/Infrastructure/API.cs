namespace eshop.webshop.Infrastructure
{
    public static class API
    {
        private const string serviceURI = "https://localhost:5001";
        public static class Authenticate
        {
            public static string Login() => $"{serviceURI}/customerauth/login";
            public static string Register() => $"{serviceURI}/customerauth/register";
        }

        public static class Customer
        {
            public static string GetCustomer(int id) => $"{serviceURI}/api/customer/{id}";
            public static string UpdateCustomer(int id) => $"{serviceURI}/api/customer/{id}";
        }

        public static class Product
        {
            public static string GetAllProduct() => $"{serviceURI}/api/product";
            public static string GetProduct(int id) => $"{serviceURI}/api/product/{id}";
            public static string GetAllProductWithCategory() => $"{serviceURI}/api/product/category";
            public static string GetAllProductComplete() => $"{serviceURI}/api/product/complete";
            public static string GetProductImages(int id) => $"{serviceURI}/api/product/{id}/image";
            public static string AddProductImages(int id) => $"{serviceURI}/api/product/{id}/image";

        }

        public static class Category
        {
            public static string GetAllCategory() => $"{serviceURI}/api/category";
            public static string GetAllCategoryWithProduct() => $"{serviceURI}/api/category/product";
        }

        public static class Orders
        {
            public static string AddOrder() => $"{serviceURI}/api/order";
            public static string GetAllOrders(int id) => $"{serviceURI}/api/order/customer/{id}";
        }
    }
}
