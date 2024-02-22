using Microsoft.AspNetCore.Http;

namespace QueflityMVC.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file);

    void DeleteImage(string relativeImagePath);
}