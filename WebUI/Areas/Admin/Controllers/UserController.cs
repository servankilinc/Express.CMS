using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.User_;
using Model.Entities;
using WebUI.Areas.Admin.Models.ViewModels.User_;
using WebUI.Utils.ActionFilters;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new UserViewModel
            {
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> CreateForm()
        {
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var viewModel = new UserCreateViewModel
            {
                RoleSelectList = allRoles.Select(r => new SelectListItem(r, r)).ToList()
            };
            return PartialView("./Partials/CreateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<UserCreateDto>))]
        public async Task<IActionResult> Create(UserCreateDto createModel)
        {
            var result = await _userService.CreateAsync(createModel);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateForm(Guid id)
        {
            var model = await _userService.GetAsync<UserUpdateDto>(where: (f) => f.Id == id);
            if (model == null) return NotFound(model);

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null) return NotFound(user);

            var existRoles = await _userManager.GetRolesAsync(user);

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var viewModel = new UserUpdateViewModel
            {
                RoleSelectList = allRoles.Select(r => new SelectListItem(r, r, r != null && existRoles.Contains(r))).ToList(),
                UpdateModel = model
            };

            return PartialView("./Partials/UpdateForm", viewModel);
        }

        [HttpPost, ServiceFilter(typeof(ValidationFilter<UserUpdateDto>))]
        public async Task<IActionResult> Update(UserUpdateDto updateModel)
        {
            var result = await _userService.UpdateAsync(updateModel);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _userService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _userService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _userService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}