using Core.Model;
using FluentValidation;

namespace Model.Dtos.Link_
{
    public class LinkUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string Icon { get; set; } = null !;
        public string Url { get; set; } = null !;
    }

    public class LinkUpdateDtoValidator : AbstractValidator<LinkUpdateDto>
    {
        public LinkUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Icon).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Url).NotEmpty().WithMessage("Bu alan boş geçilemez.");
        }
    }
}