using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.HomeSection_;

namespace WebUI.Areas.Admin.Models.ViewModels.HomeSection_
{
    public class HomeSectionCreateViewModel
    {
        public HomeSectionCreateDto CreateModel { get; set; } = new HomeSectionCreateDto();
    }
}