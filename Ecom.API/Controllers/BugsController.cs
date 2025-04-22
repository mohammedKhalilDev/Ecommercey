using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class BugsController : BaseController
    {
        public BugsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var cat = await work.CategoryRepository.GetByIdAsync(500);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var cat = await work.CategoryRepository.GetByIdAsync(500);
            cat.Name = "Error";
            return Ok(cat);
        }

        [HttpGet("bad-request/{Id}")]
        public async Task<IActionResult> GetBadRequest(int Id)
        {
            return Ok();
        }

        [HttpGet("bad-request")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
