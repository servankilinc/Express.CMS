using Core.Model;
using FluentValidation;
using Model.Dtos.Design_;

namespace Model.Dtos.DetailSection_;

public class ProductDetailSectionCreateDto : IDto
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = null!;
    public int Priority { get; set; }
    public DesignCreateDto? DesignCreateModel { get; set; }


    public class ProductDetailSectionCreateDtoValidator : AbstractValidator<ProductDetailSectionCreateDto>
    {
        public ProductDetailSectionCreateDtoValidator()
        {
            RuleFor(v => v.ProductId).NotEmpty().NotNull().NotEqual(Guid.Empty).WithMessage("Ürün bilgisi boş geçilemez.");
            RuleFor(v => v.Title).NotEmpty().WithMessage("Başlık bilgisi boş geçilemez.");
        }
    }
}
