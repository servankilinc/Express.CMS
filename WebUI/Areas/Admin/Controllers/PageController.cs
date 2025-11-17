using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using Model.Dtos.Page_;
using WebUI.Areas.Admin.Models.ViewModels.Page_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        private readonly ICompanyService _companyService;
        public PageController(IPageService pageService, ICompanyService companyService)
        {
            _pageService = pageService;
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new PageViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var viewModel = new PageCreateViewModel
            {
                CreateModel = new PageCreateDto
                {
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                }
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<PageCreateDto>))]
        public async Task<IActionResult> Create(PageCreateDto createModel)
        {
            var result = await _pageService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _pageService.GetAsync<PageUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new PageUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<PageUpdateDto>))]
        public async Task<IActionResult> Update(PageUpdateDto updateModel)
        {
            var result = await _pageService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _pageService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _pageService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _pageService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}