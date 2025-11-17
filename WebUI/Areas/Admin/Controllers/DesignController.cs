using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using WebUI.Areas.Admin.Models.ViewModels.Design_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DesignController : Controller
    {
        private readonly IDesignService _designService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;
        public DesignController(IDesignService designService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _designService = designService;
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id, string? returnUrl)
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var data = await _designService.GetAsync<DesignUpdateDto>(where: (f) => f.Id == id);
            if (data == null) return NotFound(data);

            data.ReturnUrl = returnUrl ?? Url.Action("Update", "Design", new { id = id });
            var viewModel = new DesignUpdateViewModel
            {
                UpdateModel = data
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<DesignUpdateDto>))]
        public async Task<IActionResult> Update(DesignUpdateDto updateModel)
        {
            var result = await _designService.UpdateAsync(updateModel);
            return string.IsNullOrEmpty(updateModel.ReturnUrl) ? RedirectToAction("Update", new {id = updateModel.Id}) : Redirect(updateModel.ReturnUrl);
        }


        // ************** Dosya Islemleri ************** 

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> file, Guid designId)
        {
            var uploads = new List<object>();

            foreach (var f in file)
            {
                if (f.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, $"uploads/design/{designId}");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid() + Path.GetExtension(f.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);

                    await f.CopyToAsync(stream);

                    uploads.Add(new
                    {
                        url = $"/uploads/design/{designId}/{fileName}",
                        name = f.FileName,
                        type = f.ContentType,
                        size = f.Length
                    });
                }
            }
            return Json(uploads);
        }

        [HttpGet]
        public IActionResult FileList(Guid designId)
        {
            var uploads = new List<object>();
            var sourceDirectory = Path.Combine(_environment.WebRootPath, $"uploads/design/{designId}");

            if (!Directory.Exists(sourceDirectory)) return Json(uploads);

            var files = Directory.GetFiles(sourceDirectory);
            foreach (var filePath in files)
            {
                var fileInfo = new FileInfo(filePath);
                
                uploads.Add(new
                {
                    url = $"/uploads/design/{designId}/{fileInfo.Name}",
                    name = fileInfo.Name,
                    type = "image",
                    size = fileInfo.Length
                });
            }

            return Json(uploads);
        }

        [HttpPost]
        public IActionResult FileDelete(Guid designId, string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return BadRequest(new { success = false, message = "Dosya adı bulunamadı." });

            var uploadsFolder = Path.Combine(_environment.WebRootPath, $"uploads/design/{designId}");
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Json(new { success = true, message = "Dosya silindi." });
            }

            return NotFound(new { success = false, message = "Dosya bulunamadı." });
        }

        //private string GetMimeType(string extension)
        //{
        //    var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        //    if (!provider.TryGetContentType("file" + extension, out var contentType))
        //    {
        //        contentType = "application/octet-stream"; // bilinmeyen dosya tipi
        //    }
        //    return contentType;
        //}
    }
}