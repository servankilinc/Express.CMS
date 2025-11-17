using Core.Model;
using Model.Dtos.SubMenu_;

namespace Model.Dtos.Menu_
{
    public class MenuDto : IDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null !;
        public string? Url { get; set; }
        public int Priority { get; set; }
        public string? Description { get; set; }
        public List<SubMenuDto>? SubMenuList { get; set; }
    }
}