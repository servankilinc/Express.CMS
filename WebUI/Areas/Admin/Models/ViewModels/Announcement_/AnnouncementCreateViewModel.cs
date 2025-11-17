using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Announcement_;

namespace WebUI.Areas.Admin.Models.ViewModels.Announcement_
{
    public class AnnouncementCreateViewModel
    {
        public AnnouncementCreateDto CreateModel { get; set; } = new AnnouncementCreateDto();
    }
}