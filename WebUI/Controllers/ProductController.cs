using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.DynamicQuery;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace WebUI.Controllers
{
    [Route("product")]
    [Route("urun")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService;
        public ProductController(IProductService productService, IProductGroupService productGroupService)
        {
            _productService = productService;
            _productGroupService = productGroupService;
        }
        
        [Route("/uretim/{*path}")]
        public async Task<IActionResult> Render(string path)
        { 
            var productGroup = await _productGroupService.GetAsync(f => f.PathName == path);
            if(productGroup == null) return NotFound();

            return await List(1, productGroup.Id);
        }


        public async Task<IActionResult> List(int page = 1, Guid? productGroupId = default)
        {
            var dynamicRequest = new DynamicPaginationRequest
            {
                PaginationRequest = new PaginationRequest
                {
                    Page = page,
                    PageSize = 8
                },
                Sorts = new List<Sort>()
                {
                    new Sort()
                    {
                        Dir = "asc",
                        Field = "Priority",
                    }
                }
            };
            if (productGroupId != default)
            {
                dynamicRequest.Filter = new Filter
                {
                    Field = "ProductGroupId",
                    Operator = "eq",
                    Value = productGroupId.ToString(),
                };
            }
            PaginationResponse<Product> productList = await _productService.GetListAsync(dynamicRequest);

            ViewBag.ProductGroupId = productGroupId;
            return View("List", productList);
        }

        [Route("detay/{id}")]
        [Route("detail/{id?}")]
        public async Task<IActionResult> Detail(string id)
        {
            var product = await _productService.GetByBasicAsync(friendlyUrl: id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
