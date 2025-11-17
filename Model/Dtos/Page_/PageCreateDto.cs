using Core.Model;
using Model.Dtos.Design_;
using FluentValidation;

namespace Model.Dtos.Page_
{
    public class PageCreateDto : IDto
    {
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null !;
        public bool ShowFooter { get; set; } = false;
        public int Priority { get; set; }
        public DesignCreateDto? DesignCreateModel { get; set; }
    }

    public class PageCreateDtoValidator : AbstractValidator<PageCreateDto>
    {
        public PageCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
            RuleFor(v => v.PathName).NotEmpty().WithMessage("Path bilgisi boş geçilemez.");
        }
    }
}