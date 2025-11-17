using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.Solution_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;
        public SolutionController(ISolutionService solutionService) => _solutionService = solutionService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _solutionService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _solutionService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _solutionService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetByBasic")]
        public async Task<IActionResult> GetByBasic(Guid id)
        {
            var result = await _solutionService.GetByBasicAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAllByBasic")]
        public async Task<IActionResult> GetAllByBasic(DynamicRequest? request)
        {
            var result = await _solutionService.GetAllByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetListByBasic")]
        public async Task<IActionResult> GetListByBasic(DynamicPaginationRequest request)
        {
            var result = await _solutionService.GetListByBasicAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(SolutionCreateDto request)
        {
            var result = await _solutionService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(SolutionUpdateDto request)
        {
            var result = await _solutionService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _solutionService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _solutionService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _solutionService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}