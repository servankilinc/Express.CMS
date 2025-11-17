using Business.Abstract;
using Business.Concrete;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Menu_;
using WebUI.Areas.Admin.Models.ViewModels.Menu_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ISubMenuService _subMenuService;
        public MenuController(IMenuService menuService, ISubMenuService subMenuService)
        {
            _menuService = menuService;
            _subMenuService = subMenuService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new MenuViewModel
            {
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
            var viewModel = new MenuCreateViewModel
            {
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<MenuCreateDto>))]
        public async Task<IActionResult> Create(MenuCreateDto createModel)
        {
            var result = await _menuService.CreateAsync(createModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var data = await _menuService.GetAsync<MenuUpdateDto>(where: (f) => f.Id == id);
            if (data == null)
                return NotFound(data);
            var viewModel = new MenuUpdateViewModel
            {
                UpdateModel = data
            };
            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<MenuUpdateDto>))]
        public async Task<IActionResult> Update(MenuUpdateDto updateModel)
        {
            var result = await _menuService.UpdateAsync(updateModel);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _menuService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _menuService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _menuService.DatatableServerSideAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> MenuPriority()
        {
            var data = await _menuService.GetListAsync(where: f => true);
            if (data == null) return NotFound(data);

            return PartialView("./Partials/MenuPriority", data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMenuPriority(ICollection<Guid> sortedList)
        {
            await _menuService.SetMenuPrioritiesAsync(sortedList);
            return Ok();
        }



        [HttpGet]
        public async Task<IActionResult> SubMenuPriority(Guid menuId)
        {
            var data = await _subMenuService.GetListAsync(where: f => f.MenuId == menuId);
            if (data == null) return NotFound(data);

            return PartialView("./Partials/SubMenuPriority", data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubMenuPriority(ICollection<Guid> sortedList)
        {
            await _menuService.SetSubMenuPrioritiesAsync(sortedList);
            return Ok();
        }
    }
}