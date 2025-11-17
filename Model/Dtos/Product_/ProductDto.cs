using Core.Model;
using Model.Dtos.Design_;
using Model.Dtos.DetailSection_;

namespace Model.Dtos.Product_
{
    public class ProductDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ProductGroupId { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null !;
        public string? FriendlyUrl { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null !;
        public DesignRenderDto? Design { get; set; }
        public ICollection<DetailSectionDto>? DetailSections { get; set; }
    }
}