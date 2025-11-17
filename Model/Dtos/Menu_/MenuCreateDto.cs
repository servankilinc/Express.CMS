using Core.Model;
using FluentValidation;

namespace Model.Dtos.Menu_
{
    public class MenuCreateDto : IDto
    {
        public string Title { get; set; } = null !;
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int Priority { get; set; }
    }

    public class MenuCreateDtoValidator : AbstractValidator<MenuCreateDto>
    {
        public MenuCreateDtoValidator()
        {
            RuleFor(v => v.Title).MinimumLength(2).WithMessage("En az 2 karakter içermeli.");
        }
    }
}