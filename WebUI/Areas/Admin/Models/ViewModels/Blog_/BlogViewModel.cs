using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Areas.Admin.Models.ViewModels.Blog_
{
    public class BlogViewModel
    {
        public SelectList? AuthorList { get; set; }
        public BlogFilterModel FilterModel { get; set; } = new BlogFilterModel();
    }

    public class BlogFilterModel
    {
        public Guid? AuthorId { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}