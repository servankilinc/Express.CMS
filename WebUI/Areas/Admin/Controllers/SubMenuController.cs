using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Areas.Admin.Models.ViewModels.SubMenu_;
using Model.Dtos.SubMenu_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SubMenuController : Controller
    {
        private readonly ISubMenuService _subMenuService;
        private readonly IMenuService _menuService;
        public SubMenuController(ISubMenuService subMenuService, IMenuService menuService)
        {
            _subMenuService = subMenuService;
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new SubMenuViewModel
            {
                MenuList = await _menuService.GetSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateForm()
        {
            var viewModel = new SubMenuCreateViewModel
            {
                MenuList = await _menuService.GetSelectListAsync()
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SubMenuCreateDto>))]
        public async Task<IActionResult> Create(SubMenuCreateDto createModel)
        {
            var result = await _subMenuService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _subMenuService.GetAsync<SubMenuUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new SubMenuUpdateViewModel
            {
                UpdateModel = data,
                MenuList = await _menuService.GetSelectListAsync()
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SubMenuUpdateDto>))]
        public async Task<IActionResult> Update(SubMenuUpdateDto updateModel)
        {
            var result = await _subMenuService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _subMenuService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _subMenuService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _subMenuService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}