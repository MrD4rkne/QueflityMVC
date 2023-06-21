using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public override DbSet<Image> Table() => _dbContext.Images;

        public ImageRepository(Context dbContext) : base(dbContext) { }

        public string SaveImageData(IFormFile formFile)
        {
            string directory = Environment.CurrentDirectory;
            string fileName = GetNewFileName();
            string fullPath = Path.Combine(directory, fileName + ".jpg");

            formFile.CopyTo(File.OpenWrite(fullPath));

            return fullPath;
        }

        private string GetNewFileName()
        {
            string fileName;

            do
            {
                fileName = Path.GetRandomFileName();
            } while (File.Exists(fileName));

            return fileName;
        }

        public byte[] LoadImageData(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File cannot be found");

            return File.ReadAllBytes(path);
        }
    }
}
