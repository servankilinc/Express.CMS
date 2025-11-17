using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.Product_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) => _productService = productService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _productService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _productService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _productService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

 

        [HttpPost("GetAllByBasic")]
        public async Task<IActionResult> GetAllByBasic(DynamicRequest? request)
        {
            var result = await _productService.GetAllByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByBasic")]
        public async Task<IActionResult> GetListByBasic(DynamicPaginationRequest request)
        {
            var result = await _productService.GetListByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductCreateDto request)
        {
            var result = await _productService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(ProductUpdateDto request)
        {
            var result = await _productService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _productService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _productService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}