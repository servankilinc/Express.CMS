using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.Design_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;
        public DesignController(IDesignService designService) => _designService = designService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _designService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _designService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _designService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByBasic")]
        public async Task<IActionResult> GetByBasic(Guid id)
        {
            var result = await _designService.GetByBasicAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByBasic")]
        public async Task<IActionResult> GetAllByBasic(DynamicRequest? request)
        {
            var result = await _designService.GetAllByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByBasic")]
        public async Task<IActionResult> GetListByBasic(DynamicPaginationRequest request)
        {
            var result = await _designService.GetListByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DesignCreateDto request)
        {
            var result = await _designService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(DesignUpdateDto request)
        {
            var result = await _designService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _designService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _designService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _designService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}