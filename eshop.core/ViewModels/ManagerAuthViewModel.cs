using eshop.core.JwtSettings;

namespace eshop.core.ViewModels
{
    public class ManagerAuthViewModel
    {
        public ManagerViewModel Manager { get; set; }

        public JwtAuthResult JwtResult { get; set; }
    }
}
