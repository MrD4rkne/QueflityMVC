using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<ItemImage>, IImageRepository
    {
        public ImageRepository(Context dbContext) : base(dbContext) { }
    }
}
