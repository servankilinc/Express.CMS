using Core.Model;

namespace Model.Dtos.Referance_
{
    public class ReferanceDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ReferanceGroupId { get; set; }
        public string Name { get; set; } = null !;
        public string Image { get; set; } = null !;
    }
}