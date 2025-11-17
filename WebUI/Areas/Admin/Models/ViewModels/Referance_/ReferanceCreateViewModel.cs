using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Referance_;

namespace WebUI.Areas.Admin.Models.ViewModels.Referance_
{
    public class ReferanceCreateViewModel
    {
        public ReferanceCreateDto CreateModel { get; set; } = new ReferanceCreateDto();
        public SelectList? ReferanceGroupList { get; set; }
    }
}