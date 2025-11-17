using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Design_;

namespace WebUI.Areas.Admin.Models.ViewModels.Design_
{
    public class DesignUpdateViewModel
    {
        public DesignUpdateDto UpdateModel { get; set; } = new DesignUpdateDto();
    }
}