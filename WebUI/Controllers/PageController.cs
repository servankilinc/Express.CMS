using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Page_;

namespace WebUI.Controllers;

public class PageController : Controller
{
    private readonly ILogger<PageController> _logger;
    private readonly IPageService _pageService;
    public PageController(ILogger<PageController> logger, IPageService pageService)
    {
        _logger = logger;
        _pageService = pageService;
    }

    //[Route("{*path}")]
    public async Task<IActionResult> Render(string path)
    {
        var page = await _pageService.GetBasicByNameAsync(path);

        if (page == null || page.DesignModel == null)
        {
            return NotFound();
        }

        return View(page);
    }
}
