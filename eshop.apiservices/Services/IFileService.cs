using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eshop.apiservices.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile img);
    }
}