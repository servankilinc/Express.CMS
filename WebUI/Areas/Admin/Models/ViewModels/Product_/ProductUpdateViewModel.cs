using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Product_;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_
{
    public class ProductUpdateViewModel
    {
        public ProductUpdateDto UpdateModel { get; set; } = new ProductUpdateDto();
        public SelectList? ProductGroupList { get; set; }
    }
}