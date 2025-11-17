using Core.Model;
using FluentValidation;

namespace Model.Dtos.ReferanceGroup_
{
    public class ReferanceGroupCreateDto : IDto
    {
        public string Name { get; set; } = null !;
    }

    public class ReferanceGroupCreateDtoValidator : AbstractValidator<ReferanceGroupCreateDto>
    {
        public ReferanceGroupCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}