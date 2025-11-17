using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.ReferanceGroup_;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class ReferenceController : Controller
    {
        private readonly IReferanceService _referanceService;
        private readonly IReferanceGroupService _referanceGroupService;

        public ReferenceController(IReferanceService referanceService, IReferanceGroupService referanceGroupService)
        {
            _referanceService = referanceService;
            _referanceGroupService = referanceGroupService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ReferencesViewModel
            {
                ReferenceGroupList = await _referanceGroupService.GetListAsync<ReferanceGroupDetailDto>()
            };
            return View(viewModel);
        }
    }
}
