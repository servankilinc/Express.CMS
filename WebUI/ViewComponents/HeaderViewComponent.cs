using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Menu_;

namespace WebSite.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    private readonly IMenuService _menuService;

    public HeaderViewComponent(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var menuList = await _menuService.GetAllByBasicAsync();
        return View(menuList ?? new List<MenuDto>());
    }
}
