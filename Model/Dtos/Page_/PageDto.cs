using Core.Model;
using Model.Dtos.Design_;

namespace Model.Dtos.Page_
{
    public class PageDto : IDto
    {
        public Guid Id { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
        public bool ShowFooter { get; set; } = false;
        public DesignDto? DesignModel { get; set; }
    }
}