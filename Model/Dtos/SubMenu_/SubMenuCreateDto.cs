using Core.Model;
using FluentValidation;

namespace Model.Dtos.SubMenu_
{
    public class SubMenuCreateDto : IDto
    {
        public Guid MenuId { get; set; }
        public string Title { get; set; } = null !;
        public string Url { get; set; } = null !;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int Priority { get; set; }
    }

    public class SubMenuCreateDtoValidator : AbstractValidator<SubMenuCreateDto>
    {
        public SubMenuCreateDtoValidator()
        {
            RuleFor(v => v.MenuId).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter içermeli.");
            RuleFor(v => v.Url).MinimumLength(2).WithMessage("Lütfen url bilgisini giriniz.");
        }
    }
}