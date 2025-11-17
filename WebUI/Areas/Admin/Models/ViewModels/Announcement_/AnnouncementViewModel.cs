using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Announcement_
{
    public class AnnouncementViewModel
    {
        public AnnouncementFilterModel FilterModel { get; set; } = new AnnouncementFilterModel();
    }

    public class AnnouncementFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}