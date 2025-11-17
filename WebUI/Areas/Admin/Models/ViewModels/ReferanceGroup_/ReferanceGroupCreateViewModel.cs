using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.ReferanceGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.ReferanceGroup_
{
    public class ReferanceGroupCreateViewModel
    {
        public ReferanceGroupCreateDto CreateModel { get; set; } = new ReferanceGroupCreateDto();
    }
}