using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Link_
{
    public class LinkViewModel
    {
        public LinkFilterModel FilterModel { get; set; } = new LinkFilterModel();
    }

    public class LinkFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}