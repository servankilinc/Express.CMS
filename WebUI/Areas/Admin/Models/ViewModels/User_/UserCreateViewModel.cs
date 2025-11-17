using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.User_;

namespace WebUI.Areas.Admin.Models.ViewModels.User_
{
    public class UserCreateViewModel
    {
        public List<SelectListItem>? RoleSelectList { get; set; }
        public UserCreateDto CreateModel { get; set; } = new UserCreateDto();
    }
}