using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface IManagerService
    {
        Task<(HttpStatusCode, List<ManagerViewModel>)> GetAllManagerAsync();
        Task<(HttpStatusCode, List<RoleViewModel>)> GetAllRoleAsync();
        Task<(HttpStatusCode, ManagerViewModel)> GetManagerById(int id);
        Task<(HttpStatusCode, ManagerViewModel)> AddNewManager(ManagerInfoRequest manager);
        Task<(HttpStatusCode, ManagerViewModel)> UpdateManager(ManagerInfoRequest manager);
        Task<HttpStatusCode> DeleteManager(int id);
    }
}