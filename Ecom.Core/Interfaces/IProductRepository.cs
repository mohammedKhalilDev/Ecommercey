using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDTO product);
        Task<bool> UpdateAsync(UpdateProductDTO product);
        Task DeleteAsync(Product product);

    }
}
