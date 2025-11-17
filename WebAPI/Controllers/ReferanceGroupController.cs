using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.ReferanceGroup_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class ReferanceGroupController : ControllerBase
    {
        private readonly IReferanceGroupService _referanceGroupService;
        public ReferanceGroupController(IReferanceGroupService referanceGroupService) => _referanceGroupService = referanceGroupService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _referanceGroupService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _referanceGroupService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _referanceGroupService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByBasic")]
        public async Task<IActionResult> GetByBasic(Guid id)
        {
            var result = await _referanceGroupService.GetByBasicAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByBasic")]
        public async Task<IActionResult> GetAllByBasic(DynamicRequest? request)
        {
            var result = await _referanceGroupService.GetAllByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByBasic")]
        public async Task<IActionResult> GetListByBasic(DynamicPaginationRequest request)
        {
            var result = await _referanceGroupService.GetListByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByDetail")]
        public async Task<IActionResult> GetByDetail(Guid id)
        {
            var result = await _referanceGroupService.GetByDetailAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByDetail")]
        public async Task<IActionResult> GetAllByDetail(DynamicRequest? request)
        {
            var result = await _referanceGroupService.GetAllByDetailAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByDetail")]
        public async Task<IActionResult> GetListByDetail(DynamicPaginationRequest request)
        {
            var result = await _referanceGroupService.GetListByDetailAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ReferanceGroupCreateDto request)
        {
            var result = await _referanceGroupService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(ReferanceGroupUpdateDto request)
        {
            var result = await _referanceGroupService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _referanceGroupService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _referanceGroupService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _referanceGroupService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}