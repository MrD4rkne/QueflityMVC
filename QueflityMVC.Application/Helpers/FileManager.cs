using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Helpers
{
    public static class FileManager
    {
        private const string RELATIVE_IMAGES_PATH = "Images";

        public static async Task<string> UploadFile(string root,IFormFile file)
        {
            string directory = GetImagesDirectory(root);
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string path = GetFileName(directory, Path.GetExtension(file.FileName));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Path.Combine("/"+RELATIVE_IMAGES_PATH, Path.GetFileName(path));
        }

        private static string GetFileName(string directory, string extension)
        {
            string path;
            do
            {
                path = Path.Combine(directory, Path.GetRandomFileName() + extension);
            } while (File.Exists(path));
            return path;
        }

        public static void DeleteImage(string root, string relativeImagePath)
        {
            string path = Path.Combine(GetRootDirectory(root), NormaliseFilePath(relativeImagePath));

            if(File.Exists(path))
                File.Delete(path);
        }

        private static string NormaliseFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            if (path.First() == '/')
                path = path.Substring(1);
            return path.Replace('/', '\\');
        }

        private static string GetRootDirectory(string root)
        {
            return Path.Combine(root, "wwwroot");
        }

        private static string GetImagesDirectory(string root)
        {
            return Path.Combine(GetRootDirectory(root), RELATIVE_IMAGES_PATH); ;
        }
    }
}
