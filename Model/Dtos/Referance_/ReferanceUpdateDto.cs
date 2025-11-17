using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Referance_
{
    public class ReferanceUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid ReferanceGroupId { get; set; }
        public string Name { get; set; } = null !;
        public string Image { get; set; } = null !;

        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class ReferanceUpdateDtoValidator : AbstractValidator<ReferanceUpdateDto>
    {
        public ReferanceUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.ReferanceGroupId).NotNull().WithMessage("Referans grub bilgisi boş geçilemez.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}