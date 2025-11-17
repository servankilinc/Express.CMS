using Core.Model;
using Model.Dtos.Design_;

namespace Model.Dtos.HomeSection_
{
    public class HomeSectionDto : IDto
    {
        public Guid Id { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null !;
        public int Priority { get; set; }
        public DesignRenderDto? DesignModel { get; set; }
    }
}