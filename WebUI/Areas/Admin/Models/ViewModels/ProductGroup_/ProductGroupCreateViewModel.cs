using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.ProductGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.ProductGroup_
{
    public class ProductGroupCreateViewModel
    {
        public ProductGroupCreateDto CreateModel { get; set; } = new ProductGroupCreateDto();
    }
}