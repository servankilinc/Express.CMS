using Core.Model;
using Model.Dtos.Design_;

namespace Model.Dtos.Solution_
{
    public class SolutionDto : IDto
    {
        public Guid Id { get; set; }
        public Guid SolutionGroupId { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null !;
        public string? FriendlyUrl { get; set; }
        public string Description { get; set; } = null !;
        public string Image { get; set; } = null !;
        public DesignRenderDto? Design { get; set; }
    }
}