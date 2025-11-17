using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Company_
{
    public class CompanyViewModel
    {
        public CompanyFilterModel FilterModel { get; set; } = new CompanyFilterModel();
    }

    public class CompanyFilterModel
    {
        public bool IsDeleted { get; set; }
    }
}