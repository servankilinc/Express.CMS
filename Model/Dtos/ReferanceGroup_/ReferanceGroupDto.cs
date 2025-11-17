using Core.Model;
using FluentValidation;

namespace Model.Dtos.ReferanceGroup_
{
    public class ReferanceGroupDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
    }

    public class ReferanceGroupDtoValidator : AbstractValidator<ReferanceGroupDto>
    {
        public ReferanceGroupDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}