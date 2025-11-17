using Core.Model;
using Model.Dtos.Solution_;

namespace Model.Dtos.SolutionGroup_
{
    public class SolutionGroupDetailDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
        public List<SolutionDto>? SolutionList { get; set; }
    }
}