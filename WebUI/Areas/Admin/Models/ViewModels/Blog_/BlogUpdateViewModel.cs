using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Blog_;

namespace WebUI.Areas.Admin.Models.ViewModels.Blog_
{
    public class BlogUpdateViewModel
    {
        public BlogUpdateDto UpdateModel { get; set; } = new BlogUpdateDto();
        public SelectList? AuthorList { get; set; }
    }
}