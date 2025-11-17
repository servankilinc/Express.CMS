using Model.Dtos.DetailSection_;
using Model.Entities;

namespace WebUI.Areas.Admin.Models.ViewModels.Product_.ProductDetailSection
{
    public class ProductDetailSectionsViewModel
    {
        public ICollection<DetailSectionDto>? DetailSections { get; set; }
        public Product Product { get; set; } = null!;
    }
}