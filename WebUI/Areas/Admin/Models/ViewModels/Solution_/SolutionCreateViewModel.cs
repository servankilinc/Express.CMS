using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Solution_;

namespace WebUI.Areas.Admin.Models.ViewModels.Solution_
{
    public class SolutionCreateViewModel
    {
        public SolutionCreateDto CreateModel { get; set; } = new SolutionCreateDto();
        public SelectList? SolutionGroupList { get; set; }
    }
}