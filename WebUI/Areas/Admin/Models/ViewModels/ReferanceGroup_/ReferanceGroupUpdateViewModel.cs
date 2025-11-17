using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.ReferanceGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.ReferanceGroup_
{
    public class ReferanceGroupUpdateViewModel
    {
        public ReferanceGroupUpdateDto UpdateModel { get; set; } = new ReferanceGroupUpdateDto();
    }
}