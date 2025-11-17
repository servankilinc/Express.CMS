using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.ContactMessage_;
using WebUI.Models.ViewModels;
using WebUI.Utils.ActionFilters;

namespace WebUI.Controllers;

public class ContactController : Controller
{
    private readonly IContactMessageService _contactMessageService;
    private readonly ICompanyService _companyService;
    public ContactController(IContactMessageService contactMessageService, ICompanyService companyService)
    {
        _contactMessageService = contactMessageService;
        _companyService = companyService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new ContactViewModel
        {
            Company = await _companyService.GetAsync(f => true),
        };
        return View(viewModel);
    }

    [HttpPost, ServiceFilter(typeof(ValidationFilter<ContactMessageCreateDto>))]
    public async Task<IActionResult> SendContactMessage(ContactMessageCreateDto sendContactMessageModel)
    {
        var result = await _contactMessageService.CreateAsync(sendContactMessageModel);

        return Ok(new { message = "Mesaj Başarıyla Kaydedildi" });
    }
}
