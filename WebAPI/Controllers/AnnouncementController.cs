using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Dtos.Announcement_;

namespace WebAPI.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementController(IAnnouncementService announcementService) => _announcementService = announcementService;
        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _announcementService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(DynamicRequest? request)
        {
            var result = await _announcementService.GetAllAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(DynamicPaginationRequest request)
        {
            var result = await _announcementService.GetListAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(AnnouncementCreateDto request)
        {
            var result = await _announcementService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> Update(AnnouncementUpdateDto request)
        {
            var result = await _announcementService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _announcementService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("DatatableClientSide")]
        public async Task<IActionResult> DatatableClientSide(DynamicRequest request)
        {
            var result = await _announcementService.DatatableClientSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("DatatableServerSide")]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _announcementService.DatatableServerSideAsync(request);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}