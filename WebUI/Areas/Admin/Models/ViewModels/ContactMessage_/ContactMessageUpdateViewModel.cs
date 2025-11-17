using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Entities;

namespace WebUI.Areas.Admin.Models.ViewModels.ContactMessage_
{
    public class ContactMessageUpdateViewModel
    {
        public ContactMessage UpdateModel { get; set; } = new ContactMessage();
    }
}