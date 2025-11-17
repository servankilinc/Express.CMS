using Model.Entities;

namespace WebUI.Models.ViewModels
{
    public class FooterViewModel
    {
        public Company? Company { get; set; }
        public ICollection<Link>? Links { get; set; }
        public ICollection<Page>? ShowablePageList { get; set; }
    }
}
