using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Link_;

namespace WebUI.Areas.Admin.Models.ViewModels.Link_
{
    public class LinkUpdateViewModel
    {
        public LinkUpdateDto UpdateModel { get; set; } = new LinkUpdateDto();
    }
}