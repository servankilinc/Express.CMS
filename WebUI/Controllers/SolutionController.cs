using Business.Abstract;
using Core.BaseRequestModels;
using Core.Utils.DynamicQuery;
using Core.Utils.Pagination;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace WebUI.Controllers
{
    [Route("solution")]
    [Route("cozum")]
    public class SolutionController : Controller
    {
        private readonly ISolutionService _solutionService;
        private readonly ISolutionGroupService _solutionGroupService;
        public SolutionController(ISolutionService solutionService, ISolutionGroupService solutionGroupService)
        {
            _solutionService = solutionService;
            _solutionGroupService = solutionGroupService;
        }

        [Route("/cozumler/{*path}")]
        public async Task<IActionResult> Render(string path)
        {
            var solutionGroup = await _solutionGroupService.GetAsync(f => f.PathName == path);
            if (solutionGroup == null) return NotFound();

            return await List(1, solutionGroup.Id);
        }

        public async Task<IActionResult> List(int page = 1, Guid? solutionGroupId = default)
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
            if (solutionGroupId != default)
            {
                dynamicRequest.Filter = new Filter
                {
                    Field = "SolutionGroupId",
                    Operator = "eq",
                    Value = solutionGroupId.ToString(),
                };
            }
            PaginationResponse<Solution> solutionList = await _solutionService.GetListAsync(dynamicRequest);

            ViewBag.SolutionGroupId = solutionGroupId;
            return View("List", solutionList);
        }


        [Route("detay/{id}")]
        [Route("detail/{id?}")]
        public async Task<IActionResult> Detail(string id)
        {
            var solution = await _solutionService.GetByBasicAsync(friendlyUrl: id);
            if (solution == null) return NotFound();

            return View(solution);
        }
    }
}
