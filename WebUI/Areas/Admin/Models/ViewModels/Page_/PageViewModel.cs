using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Page_
{
    public class PageViewModel
    {
        public PageFilterModel FilterModel { get; set; } = new PageFilterModel();
    }

    public class PageFilterModel
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}