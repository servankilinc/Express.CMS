using Core.Utils.Pagination;
using Model.Dtos.Blog_;

namespace WebUI.Models.ViewModels
{
    public class RecentBlogsViewModel
    { 
        public ICollection<BlogDto>? BlogList { get; set; }
    }
}
