using Core.Model;
using Model.Dtos.Design_;

namespace Model.Dtos.Project_
{
    public class ProjectDto : IDto
    {
        public Guid Id { get; set; }
        public Guid? DesignId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public DesignDto? Design { get; set; }
    }
}