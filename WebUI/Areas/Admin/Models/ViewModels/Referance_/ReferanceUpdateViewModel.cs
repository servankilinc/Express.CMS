using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Referance_;

namespace WebUI.Areas.Admin.Models.ViewModels.Referance_
{
    public class ReferanceUpdateViewModel
    {
        public ReferanceUpdateDto UpdateModel { get; set; } = new ReferanceUpdateDto();
        public SelectList? ReferanceGroupList { get; set; }
    }
}