using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.SubMenu_;

namespace WebUI.Areas.Admin.Models.ViewModels.SubMenu_
{
    public class SubMenuUpdateViewModel
    {
        public SubMenuUpdateDto UpdateModel { get; set; } = new SubMenuUpdateDto();
        public SelectList? MenuList { get; set; }
    }
}