using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Solution_
{
    public class SolutionViewModel
    {
        public SelectList? SolutionGroupList { get; set; }
        public SolutionFilterModel FilterModel { get; set; } = new SolutionFilterModel();
    }

    public class SolutionFilterModel
    {
        public Guid? SolutionGroupId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}