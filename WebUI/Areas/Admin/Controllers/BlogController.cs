using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.ExceptionHandle.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Blog_;
using Model.Dtos.Company_;
using WebUI.Areas.Admin.Models.ViewModels.Blog_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;
        public BlogController(IBlogService blogService, IUserService userService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _blogService = blogService;
            _userService = userService;
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new BlogViewModel
            {
                AuthorList = await _userService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.CKEditorLicenseKey = companyLicenses.CKEditorLicenseKey;

            var viewModel = new BlogCreateViewModel
            {
                AuthorList = await _userService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<BlogCreateDto>))]
        public async Task<IActionResult> Create(BlogCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/blogs");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Image = "/uploads/blogs/" + newFileName;
            var result = await _blogService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.CKEditorLicenseKey = companyLicenses.CKEditorLicenseKey;

            var data = await _blogService.GetAsync<BlogUpdateDto>(where: (f) => f.Id == id);
            if (data == null) return NotFound(data);

            var viewModel = new BlogUpdateViewModel
            {
                UpdateModel = data,
                AuthorList = await _userService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<BlogUpdateDto>))]
        public async Task<IActionResult> Update(BlogUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/blogs");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                var extension = Path.GetExtension(updateModel.ImageFile.FileName);
                var newFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await updateModel.ImageFile.CopyToAsync(stream);

                updateModel.Image = "/uploads/blogs/" + newFileName;
            }

            var result = await _blogService.UpdateAsync(updateModel);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _blogService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _blogService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _blogService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}