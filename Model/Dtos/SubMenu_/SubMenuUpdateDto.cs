using Core.Model;
using FluentValidation;

namespace Model.Dtos.SubMenu_
{
    public class SubMenuUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string Title { get; set; } = null !;
        public string Url { get; set; } = null !;
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }

    public class SubMenuUpdateDtoValidator : AbstractValidator<SubMenuUpdateDto>
    {
        public SubMenuUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.MenuId).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("En az 2 karakter içermeli.");
            RuleFor(v => v.Url).MinimumLength(2).WithMessage("Lütfen bu alanı doldurunuz.");
        }
    }
}