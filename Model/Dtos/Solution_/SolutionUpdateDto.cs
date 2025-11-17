using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Solution_
{
    public class SolutionUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public Guid SolutionGroupId { get; set; }
        public string Name { get; set; } = null !;
        public string? FriendlyUrl { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; } = null !;
        public int Priority { get; set; }

        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class SolutionUpdateDtoValidator : AbstractValidator<SolutionUpdateDto>
    {
        public SolutionUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.SolutionGroupId).NotNull().WithMessage("Çözüm grubu bilgisi boş geçilemez.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}