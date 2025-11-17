using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Solution_;

namespace WebUI.Areas.Admin.Models.ViewModels.Solution_
{
    public class SolutionUpdateViewModel
    {
        public SolutionUpdateDto UpdateModel { get; set; } = new SolutionUpdateDto();
        public SelectList? SolutionGroupList { get; set; }
    }
}