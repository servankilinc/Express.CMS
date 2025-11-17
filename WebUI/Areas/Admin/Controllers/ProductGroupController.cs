using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Areas.Admin.Models.ViewModels.ProductGroup_;
using Model.Dtos.ProductGroup_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _productGroupService;
        private readonly IProductService _productService;
        public ProductGroupController(IProductGroupService productGroupService, IProductService productService)
        {
            _productGroupService = productGroupService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ProductGroupViewModel
            {
            };
            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new ProductGroupCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductGroupCreateDto>))]
        public async Task<IActionResult> Create(ProductGroupCreateDto createModel)
        {
            var result = await _productGroupService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _productGroupService.GetAsync<ProductGroupUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new ProductGroupUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<ProductGroupUpdateDto>))]
        public async Task<IActionResult> Update(ProductGroupUpdateDto updateModel)
        {
            var result = await _productGroupService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _productGroupService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _productGroupService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _productGroupService.DatatableServerSideAsync(request);
            return Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> ProductPriority(Guid productGroupId)
        {
            var data = await _productService.GetListAsync(where: f => f.ProductGroupId == productGroupId);
            if (data == null) return NotFound(data); 

            return PartialView("./Partials/ProductPriority", data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductPriority(ICollection<Guid> sortedList)
        {
            await _productGroupService.SetProductPrioritiesAsync(sortedList);
            return Ok();
        }
    }
}