using Core.Model;

namespace Model.Dtos.SolutionGroup_
{
    public class SolutionGroupDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
    }
}