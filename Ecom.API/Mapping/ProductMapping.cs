using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.CategoryName, pn => pn.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();

            //CreateMap<AddProductDTO, Product>()
            //    .ForMember(p => p.photos, p => p.Ignore())
            //    .ReverseMap();

            CreateMap<Product, AddProductDTO>()
                .ForMember(dest => dest.photos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.photos, opt => opt.Ignore());

            CreateMap<Product, UpdateProductDTO>()
                .ForMember(dest => dest.photos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.photos, opt => opt.Ignore());

        }
    }
}
