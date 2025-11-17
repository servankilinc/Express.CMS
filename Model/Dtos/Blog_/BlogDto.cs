using Core.Model;
using Model.Dtos.User_;

namespace Model.Dtos.Blog_
{
    public class BlogDto : IDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = null !;
        public string Content { get; set; } = null !;
        public string Image { get; set; } = null !;
        public DateTime CreateDate { get; set; }
        public UserDto? Author { get; set; }
    }
}