using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.DynamicQuery;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
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
                }
            };
            PaginationResponse<Project> projectList = await _projectService.GetListAsync(dynamicRequest);
             
            return View(projectList);
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await _projectService.GetByBasicAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
