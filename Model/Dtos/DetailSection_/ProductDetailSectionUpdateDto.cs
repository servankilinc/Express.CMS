using Core.Model;
using FluentValidation;

namespace Model.Dtos.DetailSection_;

public class ProductDetailSectionUpdateDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Title { get; set; } = null!;

    public class ProductDetailSectionUpdateDtoValidator : AbstractValidator<ProductDetailSectionUpdateDto>
    {
        public ProductDetailSectionUpdateDtoValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().NotEqual(Guid.Empty).WithMessage("İşlem İçin Yeterli Bilgi Sağlanamadı.");
            RuleFor(v => v.ProductId).NotEmpty().NotNull().NotEqual(Guid.Empty).WithMessage("Ürün bilgisi boş geçilemez.");
            RuleFor(v => v.Title).NotEmpty().WithMessage("Başlık bilgisi boş geçilemez.");
        }
    }
}
