using Core.Model;

namespace Model.Dtos.SubMenu_
{
    public class SubMenuDto : IDto
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string Title { get; set; } = null !;
        public string Url { get; set; } = null !;
        public int Priority { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
}