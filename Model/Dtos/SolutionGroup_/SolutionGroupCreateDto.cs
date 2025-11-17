using Core.Model;
using FluentValidation;

namespace Model.Dtos.SolutionGroup_
{
    public class SolutionGroupCreateDto : IDto
    {
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
    }

    public class SolutionGroupCreateDtoValidator : AbstractValidator<SolutionGroupCreateDto>
    {
        public SolutionGroupCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
            RuleFor(v => v.PathName).NotEmpty().WithMessage("Path bilgisi boş geçilemez.");
        }
    }
}