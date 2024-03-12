using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using QueflityMVC.Application.Interfaces;
using Serilog;

namespace QueflityMVC.Application.Services;

public class FileService : IFileService
{
    private const string RELATIVE_IMAGES_PATH = "Images";
    private readonly string _rootDirectory;

    public FileService(IWebHostEnvironment appEnvironment)
    {
        _rootDirectory = appEnvironment.ContentRootPath;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        string directory = GetImagesDirectory(_rootDirectory);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string path = GetFileName(directory, Path.GetExtension(file.FileName));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return Path.Combine("/" + RELATIVE_IMAGES_PATH, Path.GetFileName(path));
    }

    private string GetFileName(string directory, string extension)
    {
        string path;
        do
        {
            path = Path.Combine(directory, Path.GetRandomFileName() + extension);
        } while (File.Exists(path));
        return path;
    }

    public void DeleteImage(string relativeImagePath)
    {
        string path = Path.Combine(GetRootDirectory(_rootDirectory), NormaliseFilePath(relativeImagePath));
        try
        {
            File.Delete(path);
        }
        catch (Exception e)
        {
            Log.Error($"Error while deleting file {path}", e);
        }
    }

    private string NormaliseFilePath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }

        if (path.First() == '/')
        {
            path = path.Substring(1);
        }
        return path.Replace('/', '\\');
    }

    private string GetRootDirectory(string root)
    {
        return Path.Combine(root, "wwwroot");
    }

    private string GetImagesDirectory(string root)
    {
        return Path.Combine(GetRootDirectory(root), RELATIVE_IMAGES_PATH); ;
    }
}