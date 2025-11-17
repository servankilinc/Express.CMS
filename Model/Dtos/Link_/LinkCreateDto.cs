using Core.Model;
using FluentValidation;

namespace Model.Dtos.Link_
{
    public class LinkCreateDto : IDto
    {
        public string Name { get; set; } = null !;
        public string Icon { get; set; } = null !;
        public string Url { get; set; } = null !;
    }

    public class LinkCreateDtoValidator : AbstractValidator<LinkCreateDto>
    {
        public LinkCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Icon).NotEmpty().WithMessage("Bu alan boş geçilemez.");
            RuleFor(v => v.Url).NotEmpty().WithMessage("Bu alan boş geçilemez.");
        }
    }
}