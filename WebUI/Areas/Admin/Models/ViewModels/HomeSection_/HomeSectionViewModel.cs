using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.HomeSection_
{
    public class HomeSectionViewModel
    {
        public HomeSectionFilterModel FilterModel { get; set; } = new HomeSectionFilterModel();
    }

    public class HomeSectionFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}