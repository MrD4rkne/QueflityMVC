using Microsoft.AspNetCore.Http;

namespace QueflityMVC.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(string root, IFormFile file);

        void DeleteImage(string root, string relativeImagePath);
    }
}