using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.ExceptionHandle.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Company_;
using Model.Dtos.Design_;
using Model.Dtos.DetailSection_;
using Model.Dtos.Product_;
using WebUI.Areas.Admin.Models.ViewModels.Product_;
using WebUI.Areas.Admin.Models.ViewModels.Product_.ProductDetailSection;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService;
        private readonly IDetailSectionService _detailSectionService;
        private readonly ICompanyService _companyService;
        private readonly IWebHostEnvironment _environment;
        public ProductController(IProductService productService, IProductGroupService productGroupService, IDetailSectionService detailSectionService, ICompanyService companyService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _productGroupService = productGroupService;
            _detailSectionService = detailSectionService;
            _companyService = companyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductViewModel
            {
                ProductGroupList = await _productGroupService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var viewModel = new ProductCreateViewModel
            {
                CreateModel = new ProductCreateDto
                {
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                },
                ProductGroupList = await _productGroupService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductCreateDto>))]
        public async Task<IActionResult> Create(ProductCreateDto createModel)
        {
            if (createModel.ImageFile == null || createModel.ImageFile.Length == 0) throw new BusinessException("Lütfen geçerli bir dosya ekleyin");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/products");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var extension = Path.GetExtension(createModel.ImageFile.FileName);
            var newFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await createModel.ImageFile.CopyToAsync(stream);

            createModel.Image = "/uploads/products/" + newFileName;
            var result = await _productService.CreateAsync(createModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _productService.GetAsync<ProductUpdateDto>(where: (f) => f.Id == id);
            if (data == null) return NotFound(data);
            var viewModel = new ProductUpdateViewModel
            {
                UpdateModel = data,
                ProductGroupList = await _productGroupService.GetSelectListAsync()
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductUpdateDto>))]
        public async Task<IActionResult> Update(ProductUpdateDto updateModel)
        {
            if (updateModel.ImageFile != null && updateModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/products");
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

                updateModel.Image = "/uploads/products/" + newFileName;
            }

            var result = await _productService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _productService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _productService.DatatableServerSideAsync(request);
            return Ok(result);
        }


        #region Methods-DetailSection
        [HttpGet]
        public async Task<IActionResult> DetailSections(Guid productId)
        {
            var data = await _detailSectionService.GetListAsync<DetailSectionDto>(where: (f) => f.ProductId == productId);
            var product = await _productService.GetAsync(where: (f) => f.Id == productId);
            if (data == null || product == null) return NotFound(data);
            var viewModel = new ProductDetailSectionsViewModel
            {   
                DetailSections = data,
                Product = product,
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> CreateDetailSection(Guid productId)
        {
            var companyLicenses = await _companyService.GetAsync<CompanyLicenseKeysDto>(where: f => true);
            if (companyLicenses != null) ViewBag.GapesJSLicenseKey = companyLicenses.GapesJSLicenseKey;

            var product = await _productService.GetAsync(where: (f) => f.Id == productId);
            if (product == null) return NotFound(product);

            var viewModel = new ProductDetailSectionCreateViewModel
            {
                Product = product,
                CreateModel = new ProductDetailSectionCreateDto
                {
                    ProductId = productId,
                    DesignCreateModel = new DesignCreateDto
                    {
                        Id = Guid.NewGuid()
                    }
                },
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductDetailSectionCreateDto>))]
        public async Task<IActionResult> CreateDetailSection(ProductDetailSectionCreateDto createModel)
        {
            var result = await _detailSectionService.CreateProductAsync(createModel);
            return RedirectToAction("DetailSections", new { productId = createModel.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDetailSectionForm(Guid detailSectionId)
        {
            var data = await _detailSectionService.GetAsync<ProductDetailSectionUpdateDto>(where: (f) => f.Id == detailSectionId);
            if (data == null) return NotFound(data);
            var viewModel = new ProductDetailSectionUpdateViewModel
            {
                UpdateModel = data,
                ProductList = await _productService.GetSelectListAsync(),
            };
            return View(viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductDetailSectionUpdateDto>))]
        public async Task<IActionResult> UpdateDetailSection(ProductDetailSectionUpdateDto updateModel)
        {
            var result = await _detailSectionService.UpdateForProductAsync(updateModel);
            return RedirectToAction("DetailSections", new { productId = updateModel.ProductId });
        } 
        #endregion
    }
}