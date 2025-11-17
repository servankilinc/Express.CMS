using Business.Abstract;
using Core.BaseRequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models.ViewModels.ContactMessage_;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ContactMessageController : Controller
    {
        private readonly IContactMessageService _contactMessageService;
        public ContactMessageController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ContactMessageViewModel
            {
            };
            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            await _contactMessageService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UndoDelete(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));
            await _contactMessageService._UndoDeleteAsync(f => f.Id == id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableServerSide(DynamicDatatableServerSideRequest request)
        {
            var result = await _contactMessageService.DatatableServerSideAsync(request);
            return Ok(result);
        }
    }
}