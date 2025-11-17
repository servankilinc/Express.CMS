using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Project_;

namespace WebUI.Areas.Admin.Models.ViewModels.Project_
{
    public class ProjectCreateViewModel
    {
        public ProjectCreateDto CreateModel { get; set; } = new ProjectCreateDto();
    }
}