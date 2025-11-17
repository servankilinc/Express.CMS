using Core.Model;
using Model.Dtos.Design_;
using FluentValidation;

namespace Model.Dtos.HomeSection_
{
    public class HomeSectionCreateDto : IDto
    {
        public string Name { get; set; } = null !;
        public int Priority { get; set; }
        public DesignCreateDto? DesignCreateModel { get; set; }
    }

    public class HomeSectionCreateDtoValidator : AbstractValidator<HomeSectionCreateDto>
    {
        public HomeSectionCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Field cannot be empty");
        }
    }
}