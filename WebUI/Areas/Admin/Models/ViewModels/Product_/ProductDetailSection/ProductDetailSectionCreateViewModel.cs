using Model.Dtos.DetailSection_;
using Model.Entities;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_.ProductDetailSection
{
    public class ProductDetailSectionCreateViewModel
    {
        public ProductDetailSectionCreateDto CreateModel { get; set; } = new ProductDetailSectionCreateDto();
        public Product Product { get; set; } = null!;
    }
}