using eshop.core.ImageSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eshop.apiservices.Services
{
    public class FileService : IFileService
    {
        private readonly ImageSetting _imageSetting;
        private static readonly string folderName = Path.Combine("Resources", "Images");
        private static readonly string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        public FileService(ImageSetting imageSetting)
        {
            _imageSetting = imageSetting;
        }

        public async Task<string> UploadImageAsync(IFormFile img)
        {
            try
            {
                if (img.Length < 0 || img.Length > _imageSetting.MaxSize) return null;
                var fileName = ContentDispositionHeaderValue.Parse(img.ContentDisposition).FileName.Trim();
                var ext = Path.GetExtension(fileName).ToString().ToLower();
                if (!_imageSetting.AllowFileTypes.Contains(ext)) return null;
                var genFileName = Guid.NewGuid().ToString() + ext;

                var fullPath = Path.Combine(pathToSave, genFileName);
                var relativePath = Path.Combine(folderName, genFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                return relativePath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
