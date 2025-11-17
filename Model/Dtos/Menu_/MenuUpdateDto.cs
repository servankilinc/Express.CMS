using Core.Model;
using FluentValidation;

namespace Model.Dtos.Menu_
{
    public class MenuUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null !;
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }

    public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
    {
        public MenuUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("İsim bilgisi en az 2 karakter içermeli.");
        }
    }
}