using Core.Model;
using FluentValidation;

namespace Model.Dtos.ReferanceGroup_
{
    public class ReferanceGroupUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
    }

    public class ReferanceGroupUpdateDtoValidator : AbstractValidator<ReferanceGroupUpdateDto>
    {
        public ReferanceGroupUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}