using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Menu_;

namespace WebUI.Areas.Admin.Models.ViewModels.Menu_
{
    public class MenuCreateViewModel
    {
        public MenuCreateDto CreateModel { get; set; } = new MenuCreateDto();
    }
}