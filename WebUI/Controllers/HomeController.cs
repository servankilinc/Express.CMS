using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.ContactMessage_;
using System.Diagnostics;
using WebUI.Models;
using WebUI.Models.ViewModels;
using WebUI.Utils.ActionFilters;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeSectionService _homeSectionService;
    private readonly IContactMessageService _contactMessageService;
    public HomeController(ILogger<HomeController> logger, IHomeSectionService homeSectionService, IContactMessageService contactMessageService)
    {
        _logger = logger;
        _homeSectionService = homeSectionService;
        _contactMessageService = contactMessageService;
    }

    public async Task<IActionResult> Index()
    {
        var sections = await _homeSectionService.GetAllByBasicAsync();

        HomeViewModel viewModel = new HomeViewModel
        {
            Sections = sections,
        };

        return View(viewModel);
    }

    [HttpPost, ServiceFilter(typeof(ValidationFilter<ContactMessageCreateDto>))]
    public async Task<IActionResult> SendContactMessage(ContactMessageCreateDto request)
    {
        var result = await _contactMessageService.CreateAsync(request);

        return Ok(new {message = "Mesaj Başarıyla Kaydedildi"});
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
