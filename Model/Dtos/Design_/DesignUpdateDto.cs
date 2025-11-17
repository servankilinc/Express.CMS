using Core.Model;
using FluentValidation;

namespace Model.Dtos.Design_
{
    public class DesignUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string? Html { get; set; }
        public string? Css { get; set; }
        public string? Script { get; set; }
        public string? ProjectJson { get; set; }

        public string? ReturnUrl { get; set; }
    }

    public class DesignUpdateDtoValidator : AbstractValidator<DesignUpdateDto>
    {
        public DesignUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Html).NotEmpty().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.ProjectJson).NotEmpty().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
        }
    }
}