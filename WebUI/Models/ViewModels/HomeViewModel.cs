using Model.Dtos.HomeSection_;

namespace WebUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<HomeSectionDto>? Sections { get; set; } = new List<HomeSectionDto>();
    }
}
