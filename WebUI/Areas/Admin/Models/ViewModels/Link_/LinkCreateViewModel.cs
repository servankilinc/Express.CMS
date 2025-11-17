using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Link_;

namespace WebUI.Areas.Admin.Models.ViewModels.Link_
{
    public class LinkCreateViewModel
    {
        public LinkCreateDto CreateModel { get; set; } = new LinkCreateDto();
    }
}