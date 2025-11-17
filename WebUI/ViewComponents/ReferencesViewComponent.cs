using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.ReferanceGroup_;
using WebUI.Models.ViewModels;

namespace WebSite.ViewComponents;

public class ReferencesViewComponent : ViewComponent
{
    private readonly IReferanceService _referanceService;
    private readonly IReferanceGroupService _referanceGroupService;

    public ReferencesViewComponent(IReferanceService referanceService, IReferanceGroupService referanceGroupService)
    {
        _referanceService = referanceService;
        _referanceGroupService = referanceGroupService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new ReferencesViewModel
        {
            ReferenceGroupList = await _referanceGroupService.GetListAsync<ReferanceGroupDetailDto>()
        };
        return View(viewModel);
    }
}
