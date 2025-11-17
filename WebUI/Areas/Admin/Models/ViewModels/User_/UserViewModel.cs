using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.User_
{
    public class UserViewModel
    {
        public UserFilterModel FilterModel { get; set; } = new UserFilterModel();
    }

    public class UserFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}