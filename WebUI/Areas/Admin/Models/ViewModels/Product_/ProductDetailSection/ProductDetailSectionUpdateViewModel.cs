using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.DetailSection_;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_.ProductDetailSection
{
    public class ProductDetailSectionUpdateViewModel
    {
        public ProductDetailSectionUpdateDto UpdateModel { get; set; } = new ProductDetailSectionUpdateDto();
        public SelectList? ProductList { get; set; }
    }
}