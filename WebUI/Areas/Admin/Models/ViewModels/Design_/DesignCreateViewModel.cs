using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Design_;

namespace WebUI.Areas.Admin.Models.ViewModels.Design_
{
    public class DesignCreateViewModel
    {
        public DesignCreateDto CreateModel { get; set; } = new DesignCreateDto();
    }
}