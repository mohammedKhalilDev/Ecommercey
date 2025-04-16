using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastruture.Data;

namespace Ecom.Infrastruture.Repositories
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        public ProductRepository(AppDbContext context): base(context)
        {
            
        }
    }
}
