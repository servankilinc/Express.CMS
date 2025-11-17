using Core.Model;
using FluentValidation;

namespace Model.Dtos.Design_
{
    public class DesignCreateDto : IDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Html { get; set; }
        public string? Css { get; set; }
        public string? Script { get; set; }
        public string? ProjectJson { get; set; }
    }

    public class DesignCreateDtoValidator : AbstractValidator<DesignCreateDto>
    {
        public DesignCreateDtoValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotEqual(Guid.Empty).WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Html).NotEmpty().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.ProjectJson).NotEmpty().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
        }
    }
}