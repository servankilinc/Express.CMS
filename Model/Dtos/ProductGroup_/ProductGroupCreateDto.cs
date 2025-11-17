using Core.Model;
using FluentValidation;

namespace Model.Dtos.ProductGroup_
{
    public class ProductGroupCreateDto : IDto
    {
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
    }

    public class ProductGroupCreateDtoValidator : AbstractValidator<ProductGroupCreateDto>
    {
        public ProductGroupCreateDtoValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
            RuleFor(v => v.PathName).NotEmpty().WithMessage("Path bilgisi boş geçilemez.");
        }
    }
}