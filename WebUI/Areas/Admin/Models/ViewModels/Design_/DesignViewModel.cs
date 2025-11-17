using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Design_
{
    public class DesignViewModel
    {
        public DesignFilterModel FilterModel { get; set; } = new DesignFilterModel();
    }

    public class DesignFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}