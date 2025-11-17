using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.ExceptionHandle.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using Model.Dtos.Project_;
using WebUI.Areas.Admin.Models.ViewModels.Project_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;
        public ProjectController(IProjectService projectService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _projectService = projectService;
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ProjectViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var viewModel = new ProjectCreateViewModel
            {
                CreateModel = new ProjectCreateDto
                {
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                }
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProjectCreateDto>))]
        public async Task<IActionResult> Create(ProjectCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/projects");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Image = "/uploads/projects/" + newFileName;
            var result = await _projectService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _projectService.GetAsync<ProjectUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new ProjectUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProjectUpdateDto>))]
        public async Task<IActionResult> Update(ProjectUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/projects");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                var extension = Path.GetExtension(updateModel.ImageFile.FileName);
                var newFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await updateModel.ImageFile.CopyToAsync(stream);

                updateModel.Image = "/uploads/projects/" + newFileName;
            }

            var result = await _projectService.UpdateAsync(updateModel);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _projectService.DeleteAsync(id);
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _projectService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _projectService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}