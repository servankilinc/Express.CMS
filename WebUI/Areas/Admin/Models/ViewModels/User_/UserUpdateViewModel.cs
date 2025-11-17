using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.User_;

namespace WebUI.Areas.Admin.Models.ViewModels.User_
{
    public class UserUpdateViewModel
    {
        public List<SelectListItem>? RoleSelectList { get; set; }
        public UserUpdateDto UpdateModel { get; set; } = new UserUpdateDto();
    }
}