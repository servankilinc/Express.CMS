using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Page_;

namespace WebUI.Areas.Admin.Models.ViewModels.Page_
{
    public class PageCreateViewModel
    {
        public PageCreateDto CreateModel { get; set; } = new PageCreateDto();
    }
}