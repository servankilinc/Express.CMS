using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Blog_;

namespace WebUI.Areas.Admin.Models.ViewModels.Blog_
{
    public class BlogCreateViewModel
    {
        public BlogCreateDto CreateModel { get; set; } = new BlogCreateDto();
        public SelectList? AuthorList { get; set; }
    }
}