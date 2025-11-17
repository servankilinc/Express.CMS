using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.ContactMessage_
{
    public class ContactMessageViewModel
    {
        public ContactMessageFilterModel FilterModel { get; set; } = new ContactMessageFilterModel();
    }

    public class ContactMessageFilterModel
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public bool IsDeleted { get; set; }
    }
}