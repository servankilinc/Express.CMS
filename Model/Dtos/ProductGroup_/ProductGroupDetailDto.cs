using Core.Model;
using Model.Dtos.Product_;

namespace Model.Dtos.ProductGroup_
{
    public class ProductGroupDetailDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
        public List<ProductDto>? ProductList { get; set; }
    }
}