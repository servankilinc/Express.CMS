using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.SolutionGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.SolutionGroup_
{
    public class SolutionGroupUpdateViewModel
    {
        public SolutionGroupUpdateDto UpdateModel { get; set; } = new SolutionGroupUpdateDto();
    }
}