using Core.Model;
using Model.Dtos.Design_;

namespace Model.Dtos.DetailSection_;

public class DetailSectionDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Title { get; set; } = null!;
    public int Priority { get; set; }
    public DesignRenderDto? Design { get; set; }
}