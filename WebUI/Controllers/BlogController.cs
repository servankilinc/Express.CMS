using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.DynamicQuery;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Blog_;

namespace WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> List(int page = 1)
        {
            var dynamicRequest = new DynamicPaginationRequest
            {
                PaginationRequest = new PaginationRequest
                {
                    Page = page,
                    PageSize = 10
                },
                Sorts = new List<Sort>()
                {
                    new Sort()
                    {
                        Dir = "asc",
                        Field = "Priority",
                    }
                },
            };
            PaginationResponse<BlogDto> blogList = await _blogService.GetListByBasicAsync(dynamicRequest);
            return View(blogList);
        }


        public async Task<IActionResult> Read(Guid id)
        {
            var blog = await _blogService.GetAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }
    }
}
