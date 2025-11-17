using Business.Abstract;
using Business.Concrete;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using Model.Dtos.HomeSection_;
using WebUI.Areas.Admin.Models.ViewModels.HomeSection_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeSectionController : Controller
    {
        private readonly IHomeSectionService _homeSectionService;
        private readonly ICompanyService _companyService;
        public HomeSectionController(IHomeSectionService homeSectionService, ICompanyService companyService)
        {
            _homeSectionService = homeSectionService;
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new HomeSectionViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var viewModel = new HomeSectionCreateViewModel
            {
                CreateModel = new HomeSectionCreateDto
                {
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                }
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<HomeSectionCreateDto>))]
        public async Task<IActionResult> Create(HomeSectionCreateDto createModel)
        {
            var result = await _homeSectionService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _homeSectionService.GetAsync<HomeSectionUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new HomeSectionUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<HomeSectionUpdateDto>))]
        public async Task<IActionResult> Update(HomeSectionUpdateDto updateModel)
        {
            var result = await _homeSectionService.UpdateAsync(updateModel);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _homeSectionService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _homeSectionService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _homeSectionService.DatatableServerSideAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Priority()
        {
            var data = await _homeSectionService.GetListAsync(where: f => true);
            if (data == null) return NotFound(data);

            return PartialView("./Partials/HomeSectionPriority", data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePriority(ICollection<Guid> sortedList)
        {
            await _homeSectionService.SetPrioritiesAsync(sortedList);
            return Ok();
        }
    }
}