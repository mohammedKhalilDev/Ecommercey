using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastruture.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO product)
        {
            if (product == null)
                return false;

            var entity = mapper.Map<Product>(product);

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();

            //add photos
            var imgPaths = await imageManagementService.AddImageAsync(product.photos, product.Name);

            var photos = imgPaths.Select(p => new Photo
            {
                ImageName = p,
                ProductId = entity.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(Product product)
        {

            foreach (var photo in product.photos)
            {
                imageManagementService.DeleteImageAsync(photo.ImageName);
            }

            //context.Photos.RemoveRange(product.photos);
            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO product)
        {
            if (product == null)
                return false;

            var entity = context.Products
                .Include(p => p.Category)
                .Include(p => p.photos)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (entity == null)
                return false;

            mapper.Map(product, entity);

            var photos = await context.Photos.Where(p => p.ProductId == product.Id).ToListAsync();

            foreach (var photo in photos)
            {
                imageManagementService.DeleteImageAsync(photo.ImageName);
            }
            context.Photos.RemoveRange(photos);

            var newPathes = await imageManagementService.AddImageAsync(product.photos, product.Name);

            var newPhotos = newPathes.Select(p => new Photo
            {
                ProductId = product.Id,
                ImageName = p,
            });

            await context.Photos.AddRangeAsync(newPhotos);

            await context.SaveChangesAsync();

            return true;

        }
    }
}
