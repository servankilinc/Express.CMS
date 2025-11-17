using Core.Model;

namespace Model.Dtos.User_
{
    public class UserDto : IDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null !;
    }
}