using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.SolutionGroup_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class SolutionGroupController : ControllerBase
    {
        private readonly ISolutionGroupService _solutionGroupService;
        public SolutionGroupController(ISolutionGroupService solutionGroupService) => _solutionGroupService = solutionGroupService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _solutionGroupService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _solutionGroupService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _solutionGroupService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByBasic")]
        public async Task<IActionResult> GetByBasic(Guid id)
        {
            var result = await _solutionGroupService.GetByBasicAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByBasic")]
        public async Task<IActionResult> GetAllByBasic(DynamicRequest? request)
        {
            var result = await _solutionGroupService.GetAllByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByBasic")]
        public async Task<IActionResult> GetListByBasic(DynamicPaginationRequest request)
        {
            var result = await _solutionGroupService.GetListByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByDetail")]
        public async Task<IActionResult> GetByDetail(Guid id)
        {
            var result = await _solutionGroupService.GetByDetailAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByDetail")]
        public async Task<IActionResult> GetAllByDetail(DynamicRequest? request)
        {
            var result = await _solutionGroupService.GetAllByDetailAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByDetail")]
        public async Task<IActionResult> GetListByDetail(DynamicPaginationRequest request)
        {
            var result = await _solutionGroupService.GetListByDetailAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(SolutionGroupCreateDto request)
        {
            var result = await _solutionGroupService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(SolutionGroupUpdateDto request)
        {
            var result = await _solutionGroupService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _solutionGroupService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _solutionGroupService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _solutionGroupService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}