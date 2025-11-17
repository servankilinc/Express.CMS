using Model.Dtos.ContactMessage_;
using Model.Entities;

namespace WebUI.Models.ViewModels
{
    public class ContactViewModel
    {
        public Company? Company { get; set; }
        public ContactMessageCreateDto? SendContactMessageModel { get; set; }
    }
}
