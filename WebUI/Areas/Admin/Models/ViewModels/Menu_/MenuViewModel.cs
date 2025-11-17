using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Menu_
{
    public class MenuViewModel
    {
        public MenuFilterModel FilterModel { get; set; } = new MenuFilterModel();
    }

    public class MenuFilterModel
    {
        public string? Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}