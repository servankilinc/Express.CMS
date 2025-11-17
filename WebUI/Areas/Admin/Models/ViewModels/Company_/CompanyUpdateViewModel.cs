using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Company_;

namespace WebUI.Areas.Admin.Models.ViewModels.Company_
{
    public class CompanyUpdateViewModel
    {
        public CompanyUpdateDto UpdateModel { get; set; } = new CompanyUpdateDto();
    }
}