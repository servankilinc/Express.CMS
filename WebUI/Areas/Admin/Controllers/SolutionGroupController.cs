using Business.Abstract;
using Business.Concrete;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.SolutionGroup_;
using WebUI.Areas.Admin.Models.ViewModels.SolutionGroup_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SolutionGroupController : Controller
    {
        private readonly ISolutionGroupService _solutionGroupService;
        private readonly ISolutionService _solutionService;
        public SolutionGroupController(ISolutionGroupService solutionGroupService, ISolutionService solutionService)
        {
            _solutionGroupService = solutionGroupService;
            _solutionService = solutionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new SolutionGroupViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new SolutionGroupCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SolutionGroupCreateDto>))]
        public async Task<IActionResult> Create(SolutionGroupCreateDto createModel)
        {
            var result = await _solutionGroupService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _solutionGroupService.GetAsync<SolutionGroupUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new SolutionGroupUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SolutionGroupUpdateDto>))]
        public async Task<IActionResult> Update(SolutionGroupUpdateDto updateModel)
        {
            var result = await _solutionGroupService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _solutionGroupService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _solutionGroupService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _solutionGroupService.DatatableServerSideAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SolutionPriority(Guid solutionGroupId)
        {
            var data = await _solutionService.GetListAsync(where: f => f.SolutionGroupId == solutionGroupId);
            if (data == null) return NotFound(data);

            return PartialView("./Partials/SolutionPriority", data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSolutionPriority(ICollection<Guid> sortedList)
        {
            await _solutionGroupService.SetSolutionPrioritiesAsync(sortedList);
            return Ok();
        }
    }
}