using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Areas.Admin.Models.ViewModels.ReferanceGroup_;
using Model.Dtos.ReferanceGroup_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ReferanceGroupController : Controller
    {
        private readonly IReferanceGroupService _referanceGroupService;
        public ReferanceGroupController(IReferanceGroupService referanceGroupService)
        {
            _referanceGroupService = referanceGroupService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ReferanceGroupViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new ReferanceGroupCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ReferanceGroupCreateDto>))]
        public async Task<IActionResult> Create(ReferanceGroupCreateDto createModel)
        {
            var result = await _referanceGroupService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _referanceGroupService.GetAsync<ReferanceGroupUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new ReferanceGroupUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ReferanceGroupUpdateDto>))]
        public async Task<IActionResult> Update(ReferanceGroupUpdateDto updateModel)
        {
            var result = await _referanceGroupService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _referanceGroupService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _referanceGroupService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _referanceGroupService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}