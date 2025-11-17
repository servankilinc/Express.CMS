using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Announcement_;

namespace WebUI.Areas.Admin.Models.ViewModels.Announcement_
{
    public class AnnouncementUpdateViewModel
    {
        public AnnouncementUpdateDto UpdateModel { get; set; } = new AnnouncementUpdateDto();
    }
}