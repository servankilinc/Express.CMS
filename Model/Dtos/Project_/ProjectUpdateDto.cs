using Core.Model;
using Core.Utils.CriticalData;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Model.Dtos.Project_
{
    public class ProjectUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string Image { get; set; } = null !;
        
        [IgnoreMap]
        public IFormFile? ImageFile { get; set; }
    }

    public class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
        }
    }
}