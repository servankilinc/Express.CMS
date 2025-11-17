using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Page_;

namespace WebUI.Areas.Admin.Models.ViewModels.Page_
{
    public class PageUpdateViewModel
    {
        public PageUpdateDto UpdateModel { get; set; } = new PageUpdateDto();
    }
}