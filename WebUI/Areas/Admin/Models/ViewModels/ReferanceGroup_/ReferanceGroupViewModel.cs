using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.ReferanceGroup_
{
    public class ReferanceGroupViewModel
    {
        public ReferanceGroupFilterModel FilterModel { get; set; } = new ReferanceGroupFilterModel();
    }

    public class ReferanceGroupFilterModel
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}