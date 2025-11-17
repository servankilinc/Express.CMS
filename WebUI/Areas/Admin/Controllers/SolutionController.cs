using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.ExceptionHandle.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using Model.Dtos.Solution_;
using WebUI.Areas.Admin.Models.ViewModels.Solution_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SolutionController : Controller
    {
        private readonly ISolutionService _solutionService;
        private readonly ISolutionGroupService _solutionGroupService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;

        public SolutionController(ISolutionService solutionService, ISolutionGroupService solutionGroupService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _solutionService = solutionService;
            _solutionGroupService = solutionGroupService;
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SolutionViewModel
            {
                SolutionGroupList = await _solutionGroupService.GetSelectListAsync()
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var viewModel = new SolutionCreateViewModel
            {
                CreateModel = new SolutionCreateDto
                {
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                },
                SolutionGroupList = await _solutionGroupService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SolutionCreateDto>))]
        public async Task<IActionResult> Create(SolutionCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/solutions");

            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Image = "/uploads/solutions/" + newFileName;
            var result = await _solutionService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _solutionService.GetAsync<SolutionUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new SolutionUpdateViewModel
            {
                UpdateModel = data,
                SolutionGroupList = await _solutionGroupService.GetSelectListAsync()
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SolutionUpdateDto>))]
        public async Task<IActionResult> Update(SolutionUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/solutions");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                if (!string.IsNullOrEmpty(updateModel.Image))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, updateModel.Image.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
                }

                var extension = Path.GetExtension(updateModel.ImageFile.FileName);
                var newFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await updateModel.ImageFile.CopyToAsync(stream);

                updateModel.Image = "/uploads/solutions/" + newFileName;
            }

            var result = await _solutionService.UpdateAsync(updateModel);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _solutionService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _solutionService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _solutionService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}