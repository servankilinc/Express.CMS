using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Areas.Admin.Models.ViewModels.Referance_;
using Model.Dtos.Referance_;
using Core.Utils.ExceptionHandle.Exceptions;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ReferanceController : Controller
    {
        private readonly IReferanceService _referanceService;
        private readonly IReferanceGroupService _referanceGroupService;
        private readonly IWebHostEnvironment _environment;
        public ReferanceController(IReferanceService referanceService, IReferanceGroupService referanceGroupService, IWebHostEnvironment environment)
        {
            _referanceService = referanceService;
            _referanceGroupService = referanceGroupService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ReferanceViewModel
            {
                ReferanceGroupList = await _referanceGroupService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateForm()
        {
            var viewModel = new ReferanceCreateViewModel
            {
                ReferanceGroupList = await _referanceGroupService.GetSelectListAsync()
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ReferanceCreateDto>))]
        public async Task<IActionResult> Create(ReferanceCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/referances");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Image = "/uploads/referances/" + newFileName;
            var result = await _referanceService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _referanceService.GetAsync<ReferanceUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new ReferanceUpdateViewModel
            {
                UpdateModel = data,
                ReferanceGroupList = await _referanceGroupService.GetSelectListAsync()
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ReferanceUpdateDto>))]
        public async Task<IActionResult> Update(ReferanceUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/referances");
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

                updateModel.Image = "/uploads/referances/" + newFileName;
            }

            var result = await _referanceService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _referanceService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _referanceService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _referanceService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}