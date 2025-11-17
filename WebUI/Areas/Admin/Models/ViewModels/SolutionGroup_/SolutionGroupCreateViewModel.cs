using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.SolutionGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.SolutionGroup_
{
    public class SolutionGroupCreateViewModel
    {
        public SolutionGroupCreateDto CreateModel { get; set; } = new SolutionGroupCreateDto();
    }
}