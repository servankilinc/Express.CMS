using Business.Abstract;
using Business.Concrete;
using Core.BaseRequestModels;
using Core.Utils.ExceptionHandle.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Entities;
using WebUI.Areas.Admin.Models.ViewModels.Company_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;
        public CompanyController(ICompanyService companyService, IWebHostEnvironment environment)
        {
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new CompanyViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new CompanyCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/companies");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Logo = "/uploads/companies/" + newFileName;
            var result = await _companyService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _companyService.GetAsync<CompanyUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new CompanyUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<CompanyUpdateDto>))]
        public async Task<IActionResult> Update(CompanyUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/companies");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                var extension = Path.GetExtension(updateModel.ImageFile.FileName);
                var newFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await updateModel.ImageFile.CopyToAsync(stream);

                updateModel.Logo = "/uploads/companies/" + newFileName;
            }

            var result = await _companyService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _companyService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _companyService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _companyService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}