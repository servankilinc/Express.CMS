using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.ProductGroup_
{
    public class ProductGroupViewModel
    {
        public ProductGroupFilterModel FilterModel { get; set; } = new ProductGroupFilterModel();
    }

    public class ProductGroupFilterModel
    {
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}