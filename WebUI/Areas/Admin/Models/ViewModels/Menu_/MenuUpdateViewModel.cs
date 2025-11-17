using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Menu_;

namespace WebUI.Areas.Admin.Models.ViewModels.Menu_
{
    public class MenuUpdateViewModel
    {
        public MenuUpdateDto UpdateModel { get; set; } = new MenuUpdateDto();
    }
}