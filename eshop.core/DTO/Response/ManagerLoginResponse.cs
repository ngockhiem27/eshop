using eshop.core.ViewModels;

namespace eshop.core.DTO.Response
{
    public class ManagerLoginResponse
    {
        public ManagerViewModel Manager { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
