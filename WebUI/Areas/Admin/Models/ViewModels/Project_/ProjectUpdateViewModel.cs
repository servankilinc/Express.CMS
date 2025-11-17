using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Project_;

namespace WebUI.Areas.Admin.Models.ViewModels.Project_
{
    public class ProjectUpdateViewModel
    {
        public ProjectUpdateDto UpdateModel { get; set; } = new ProjectUpdateDto();
    }
}