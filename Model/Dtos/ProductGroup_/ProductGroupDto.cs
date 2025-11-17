using Core.Model;
using FluentValidation;

namespace Model.Dtos.ProductGroup_
{
    public class ProductGroupDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null !;
        public string PathName { get; set; } = null!;
    }

    public class ProductGroupDtoValidator : AbstractValidator<ProductGroupDto>
    {
        public ProductGroupDtoValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("İşlem için yeterli bilgi sağlanamadı.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("İsim bilgisi boş geçilemez.");
            RuleFor(v => v.PathName).NotEmpty().WithMessage("Path bilgisi boş geçilemez.");
        }
    }
}