using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebUI.Utils.ActionFilters;
using Model.Entities;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SmtpSettingsController : Controller
    {
        private readonly ISmtpSettingsService smtpSettingsService;

        public SmtpSettingsController(ISmtpSettingsService smtpSettingsService)
        {
            this.smtpSettingsService = smtpSettingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await smtpSettingsService.GetAsync();
            return View(result);
        }         

        [HttpPost, ServiceFilter(typeof(ValidationFilter<SmtpSettings>))]
        public async Task<IActionResult> Update(SmtpSettings updateModel)
        {
            var result = await smtpSettingsService.UpdateAsync(updateModel);
            return Ok(result);
        }
    }
}