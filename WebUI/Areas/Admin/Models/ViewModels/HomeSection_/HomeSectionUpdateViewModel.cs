using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.HomeSection_;

namespace WebUI.Areas.Admin.Models.ViewModels.HomeSection_
{
    public class HomeSectionUpdateViewModel
    {
        public HomeSectionUpdateDto UpdateModel { get; set; } = new HomeSectionUpdateDto();
    }
}