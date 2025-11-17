using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Referance_
{
    public class ReferanceViewModel
    {
        public SelectList? ReferanceGroupList { get; set; }
        public ReferanceFilterModel FilterModel { get; set; } = new ReferanceFilterModel();
    }

    public class ReferanceFilterModel
    {
        public Guid? ReferanceGroupId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}