using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.ViewModels;

namespace WebSite.ViewComponents;

public class RecentBlogsViewComponent : ViewComponent
{
    private readonly IBlogService _blogService;

    public RecentBlogsViewComponent(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new RecentBlogsViewModel
        {
            BlogList = await _blogService.GetAllByBasicAsync()
        };
        return View(viewModel);
    }
}
