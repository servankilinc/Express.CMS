using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.ContactMessage_;

namespace WebUI.Areas.Admin.Models.ViewModels.ContactMessage_
{
    public class ContactMessageCreateViewModel
    {
        public ContactMessageCreateDto CreateModel { get; set; } = new ContactMessageCreateDto();
    }
}