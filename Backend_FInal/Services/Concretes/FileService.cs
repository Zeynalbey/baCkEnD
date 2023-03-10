using Backend_Final.Contracts.File;
using Backend_Final.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Backend_Final.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService>? _logger;

        public FileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInSystem = GenerateUniqueFileName(formFile.FileName);
            var filePath = Path.Combine(directoryPath, imageNameInSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger!.LogError(e, "Error occured in file service");

                throw;
            }

            return imageNameInSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName);

            await Task.Run(() => File.Delete(deletePath));
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Payment:
                    return Path.Combine(startPath, "Payments");
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "Sliders");
                case UploadDirectory.Product:
                    return Path.Combine(startPath, "Products");
                case UploadDirectory.Blog:
                    return Path.Combine(startPath, "Blogs");
                default:
                    throw new Exception("Something went wrong");
            }
        }
            
        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.Payment:
                    return $"{initialSegment}/Payments/{fileName}";
                case UploadDirectory.Slider:
                    return $"{initialSegment}/Sliders/{fileName}";
                case UploadDirectory.Product:
                    return $"{initialSegment}/Products/{fileName}";
                case UploadDirectory.Blog:
                    return $"{initialSegment}/Blogs/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }
    }
}
