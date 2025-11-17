using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Project_
{
    public class ProjectViewModel
    {
        public ProjectFilterModel FilterModel { get; set; } = new ProjectFilterModel();
    }

    public class ProjectFilterModel
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}