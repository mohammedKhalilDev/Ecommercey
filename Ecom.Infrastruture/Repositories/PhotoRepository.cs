using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastruture.Data;

namespace Ecom.Infrastruture.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {

        }
    }
}
