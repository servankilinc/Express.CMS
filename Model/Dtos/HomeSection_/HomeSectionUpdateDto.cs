using Core.Model;
using FluentValidation;

namespace Model.Dtos.HomeSection_
{
    public class HomeSectionUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
    }

    public class HomeSectionUpdateDtoValidator : AbstractValidator<HomeSectionUpdateDto>
    {
        public HomeSectionUpdateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}