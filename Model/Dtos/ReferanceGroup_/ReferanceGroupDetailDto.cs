using Core.Model;
using Model.Dtos.Referance_;

namespace Model.Dtos.ReferanceGroup_
{
    public class ReferanceGroupDetailDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public List<ReferanceDto>? ReferanceList { get; set; }
    }
}