using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.SubMenu_
{
    public class SubMenuViewModel
    {
        public SelectList? MenuList { get; set; }
        public SubMenuFilterModel FilterModel { get; set; } = new SubMenuFilterModel();
    }

    public class SubMenuFilterModel
    {
        public Guid? MenuId { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}