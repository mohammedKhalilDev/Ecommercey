using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get(string? sort, int? categoryId, int pageNumber, int pageSize)
        {
            try
            {
                var cats = await work.ProductRepository
                    .GetAllAsync(sort, categoryId, pageNumber, pageSize);


                return Ok(cats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = work.ProductRepository
                    .GetByIdAsync(id, p => p.Category, p => p.photos);

                var res = mapper.Map<ProductDTO>(product);

                if (res == null)
                    return BadRequest(new ResponseAPI(400));

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> add(AddProductDTO dto)
        {
            try
            {
                var res = await work.ProductRepository.AddAsync(dto);

                if (res == false)
                    return BadRequest(new ResponseAPI(400));

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400));
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> update(UpdateProductDTO dto)
        {
            try
            {
                var res = await work.ProductRepository.UpdateAsync(dto);

                if (res == false)
                    return BadRequest(new ResponseAPI(400));

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var entity = await work.ProductRepository.GetByIdAsync(id, p => p.Category, p => p.photos);

                await work.ProductRepository.DeleteAsync(entity);

                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
