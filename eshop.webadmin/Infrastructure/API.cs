namespace eshop.webadmin.Infrastructure
{
    public static class API
    {
        private const string serviceURI = "https://localhost:5001";
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

        }

        public static class Category
        {
            public static string GetAllCategory() => $"{serviceURI}/api/category";
            public static string GetAllCategoryWithProduct() => $"{serviceURI}/api/category/product";
            public static string AddCategory() => $"{serviceURI}/api/category";
            public static string UpdateCategory(int id) => $"{serviceURI}/api/category/{id}";
            public static string DeleteCategory(int id) => $"{serviceURI}/api/category/{id}";
        }
    }
}
