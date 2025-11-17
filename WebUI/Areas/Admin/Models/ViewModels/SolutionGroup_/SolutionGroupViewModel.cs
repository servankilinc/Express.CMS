using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.SolutionGroup_
{
    public class SolutionGroupViewModel
    {
        public SolutionGroupFilterModel FilterModel { get; set; } = new SolutionGroupFilterModel();
    }

    public class SolutionGroupFilterModel
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}