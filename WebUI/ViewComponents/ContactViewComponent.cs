using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.ViewModels;

namespace WebSite.ViewComponents;

public class ContactViewComponent : ViewComponent
{
    private readonly ICompanyService _companyService;

    public ContactViewComponent(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new ContactViewModel
        {
            Company = await _companyService.GetAsync(f => true),
        };
        return View(viewModel);
    }
}
