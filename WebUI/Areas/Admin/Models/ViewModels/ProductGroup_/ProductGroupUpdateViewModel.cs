using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.ProductGroup_;

namespace WebUI.Areas.Admin.Models.ViewModels.ProductGroup_
{
    public class ProductGroupUpdateViewModel
    {
        public ProductGroupUpdateDto UpdateModel { get; set; } = new ProductGroupUpdateDto();
    }
}