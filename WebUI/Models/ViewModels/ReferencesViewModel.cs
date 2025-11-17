using Model.Dtos.Referance_;
using Model.Dtos.ReferanceGroup_;

namespace WebUI.Models.ViewModels
{
    public class ReferencesViewModel
    {
        public ICollection<ReferanceGroupDetailDto>? ReferenceGroupList { get; set; }

    }
}
