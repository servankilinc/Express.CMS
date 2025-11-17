using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Link_;
using WebUI.Areas.Admin.Models.ViewModels.Link_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class LinkController : Controller
    {
        private readonly ILinkService _linkService;
        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new LinkViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new LinkCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<LinkCreateDto>))]
        public async Task<IActionResult> Create(LinkCreateDto createModel)
        {
            var result = await _linkService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _linkService.GetAsync<LinkUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new LinkUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<LinkUpdateDto>))]
        public async Task<IActionResult> Update(LinkUpdateDto updateModel)
        {
            var result = await _linkService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _linkService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _linkService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _linkService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}