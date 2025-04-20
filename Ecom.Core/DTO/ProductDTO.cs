using Microsoft.AspNetCore.Http;

namespace Ecom.Core.DTO
{
    public record ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public virtual List<PhotoDTO> photos { get; set; }
        public string CategoryName { get; set; }

    }

    public record AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }

        public virtual IFormFileCollection photos { get; set; }

    }

    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }
}
