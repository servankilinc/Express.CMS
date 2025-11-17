using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.SubMenu_;

namespace WebUI.Areas.Admin.Models.ViewModels.SubMenu_
{
    public class SubMenuCreateViewModel
    {
        public SubMenuCreateDto CreateModel { get; set; } = new SubMenuCreateDto();
        public SelectList? MenuList { get; set; }
    }
}