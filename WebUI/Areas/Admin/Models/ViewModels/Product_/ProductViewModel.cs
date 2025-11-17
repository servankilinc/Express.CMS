using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Design_;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_
{
    public class ProductViewModel
    {
        public SelectList? ProductGroupList { get; set; }
        public ProductFilterModel FilterModel { get; set; } = new ProductFilterModel();
    }

    public class ProductFilterModel
    {
        public Guid? ProductGroupId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}