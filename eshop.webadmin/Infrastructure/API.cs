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
            public static string GetManagerById(int id) => $"{serviceURI}/api/manager/{id}";
            public static string AddManager() => $"{serviceURI}/api/manager/";
            public static string UpdateManager() => $"{serviceURI}/api/manager/";
            public static string DeleteManager(int id) => $"{serviceURI}/api/manager/{id}";
        }
    }
}
