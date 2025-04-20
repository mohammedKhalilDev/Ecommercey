using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var cats = await work.CategoryRepository.GetAllAsync();
                if (cats == null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(cats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/id{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var cats = await work.CategoryRepository.GetByIdAsync(id);
                if (cats == null)
                    return BadRequest(new ResponseAPI(400, $"Category with Id = {id} not found"));
                return Ok(cats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var cat = mapper.Map<Category>(categoryDTO);

                await work.CategoryRepository.AddAsync(cat);

                return Ok(new ResponseAPI(200, "Item Added Succesfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var cat = mapper.Map<Category>(categoryDTO);

                await work.CategoryRepository.UpdateAsync(cat);

                return Ok(new ResponseAPI(200, "Item updated Succesfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);

                return Ok(new ResponseAPI(200, "Item deleted Succesfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
