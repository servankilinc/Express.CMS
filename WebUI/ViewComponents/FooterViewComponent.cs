using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.ViewModels;

namespace WebSite.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly ICompanyService _companyService;
    private readonly ILinkService _linkService;
    private readonly IPageService _pageService;

    public FooterViewComponent(ICompanyService companyService, ILinkService linkService, IPageService pageService)
    {
        _companyService = companyService;
        _linkService = linkService;
        _pageService = pageService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new FooterViewModel
        {
            Company = await _companyService.GetAsync(f => true),
            Links = await _linkService.GetListAsync(),
            ShowablePageList = await _pageService.GetListAsync(f => f.ShowFooter == true)
        };
        return View(viewModel);
    }
}
