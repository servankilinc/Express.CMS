using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Product_;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_
{
    public class ProductCreateViewModel
    {
        public ProductCreateDto CreateModel { get; set; } = new ProductCreateDto();
        public SelectList? ProductGroupList { get; set; }
    }
}