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
    }
}
